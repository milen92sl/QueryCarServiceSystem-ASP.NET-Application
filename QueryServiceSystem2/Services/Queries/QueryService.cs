namespace QueryServiceSystem2.Services.Queries.Models
{
	using AutoMapper;
	using AutoMapper.QueryableExtensions;
	using QueryServiceSystem2.Data;
	using QueryServiceSystem2.Data.Models;
	using QueryServiceSystem2.Models;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class QueryService : IQueryService
	{
		private readonly QueryService2DbContext data;
		private readonly IConfigurationProvider mapper;

		public QueryService(QueryService2DbContext data, IMapper mapper)
		{
			this.data = data;
			this.mapper = mapper.ConfigurationProvider;
		}

		public CarQueryServiceModel All(
				string brand = null,
				int currentPage = 1,
				int queriesPerPage = int.MaxValue,
				bool publicOnly = true)
		{
			var q = this.data.Queries
				.Where(c => !publicOnly || c.IsPublic);

			if (!string.IsNullOrWhiteSpace(brand))
			{
				q = q.Where(c => c.Car.Name == brand);
			}

			var totalQueries = q.Count();

			var queries = GetQueries(q
			.Skip((currentPage - 1) * queriesPerPage)
			.Take(queriesPerPage));


			return new CarQueryServiceModel
			{
				TotalQueries = totalQueries,
				CurrentPage = currentPage,
				QueriesPerPage = queriesPerPage,
				Queries = queries
			};
		}

		public CarQueryServiceModel AllReferences(
				string brand = null,
				string searchTerm = null,
				QueriesSorting sorting = QueriesSorting.Date,
				int currentPage = 1,
				int queriesPerPage = int.MaxValue,
				bool publicOnly = true)
		{
			var q = this.data.Queries
				.Where(c => !publicOnly || c.IsPublic);

			if (!string.IsNullOrWhiteSpace(brand))
			{
				q = q.Where(c => c.Car.Name == brand);
			}

			if (!string.IsNullOrWhiteSpace(searchTerm))
			{
				q = q.Where(c =>
					(c.Brand + " " + c.Date).ToLower().Contains(searchTerm.ToLower()) ||
					c.Description.ToLower().Contains(searchTerm.ToLower()));
			}

			q = sorting switch
			{
				QueriesSorting.Brand => q.OrderBy(c => c.Brand),
				QueriesSorting.Date or _ => q.OrderByDescending(c => c.Id),
			};

			var totalQueries = q.Count();

			var queries = GetQueries(q
			.Skip((currentPage - 1) * queriesPerPage)
			.Take(queriesPerPage));


			return new CarQueryServiceModel
			{
				TotalQueries = totalQueries,
				CurrentPage = currentPage,
				QueriesPerPage = queriesPerPage,
				Queries = queries
			};
		}

		public QueryDetailsServiceModel Details(int id)
		=> this.data
			.Queries
			.Where(c => c.Id == id)
			.ProjectTo<QueryDetailsServiceModel>(this.mapper)
			.FirstOrDefault();

		public int Create(string brand, string carModel, string carRegistrationNumber, string carColor, string description, string imageUrl, DateTime date, int carId, int mechanicId)
		{
			var queryData = new Query
			{
				Brand = brand,
				Model = carModel,
				RegistrationNumber = carRegistrationNumber,
				Color = carColor,
				Description = description,
				Date = date,
				ImageUrl = imageUrl,
				CarId = carId,
				MechanicId = mechanicId,
				IsPublic = false,
			};

			this.data.Queries.Add(queryData);
			this.data.SaveChanges();

			return queryData.Id;
		}

		public IEnumerable<QueryServiceModel> ByUser(string userId)
		=> GetQueries(this.data
			.Queries
			.Where(c => c.Mechanic.UserId == userId));

		public bool IsByMechanic(int queryId, int dealerId)
		=> this.data
			.Queries
			.Any(c => c.Id == queryId && c.MechanicId == dealerId);

		public IEnumerable<string> AllBrands()
			=> this.data
				.Queries
				.Select(c => c.Car.Name)
				.Distinct()
				.OrderBy(br => br)
				.ToList();

		public IEnumerable<QueryCarServiceModel> AllCars()
		=> this.data
		 .Cars
		 .ProjectTo<QueryCarServiceModel>(this.mapper)
		 .ToList();

		public bool CarExists(int carId)
		=> this.data
			.Cars
			.Any(c => c.Id == carId);

		private IEnumerable<QueryServiceModel> GetQueries(IQueryable<Query> qQuery)
		=> qQuery
			.ProjectTo<QueryServiceModel>(this.mapper)
			.ToList();

		public bool Edit(
			int id,
			string brand,
			string carModel,
			string carRegistrationNumber,
			string carColor,
			string description,
			string imageUrl,
			DateTime date,
			int carId,
			bool isPublic)
		{
			var queryData = this.data.Queries.Find(id);

			if (queryData == null)
			{
				return false;
			}

			queryData.Brand = brand;
			queryData.Model = carModel;
			queryData.RegistrationNumber = carRegistrationNumber;
			queryData.Color = carColor;
			queryData.Description = description;
			queryData.ImageUrl = imageUrl;
			queryData.Date = date;
			queryData.CarId = carId;
			queryData.IsPublic = false;

			this.data.SaveChanges();

			return true;
		}

		public IEnumerable<LatestQueryServiceModel> Latest()
		   => this.data
				  .Queries
				  .Where(c => c.IsPublic)
				  .OrderByDescending(c => c.Id)
				  .ProjectTo<LatestQueryServiceModel>(mapper)
				  .Take(data.Cars.Count())
				  .ToList();

		public void ChangeVisibility(int carId)
		{
			var car = this.data.Queries.Find(carId);

			car.IsPublic = !car.IsPublic;

			this.data.SaveChanges();
		}
	}
}