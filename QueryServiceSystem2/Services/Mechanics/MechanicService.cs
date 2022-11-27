using AutoMapper;
using AutoMapper.QueryableExtensions;
using QueryServiceSystem2.Data;
using QueryServiceSystem2.Data.Models;
using QueryServiceSystem2.Services.Mechanics.Models;
using QueryServiceSystem2.Services.Queries.Models;
using System.Collections.Generic;
using System.Linq;

namespace QueryServiceSystem2.Services.Mechanics
{
	public class MechanicService : IMechanicService
	{
		private readonly QueryService2DbContext data;
		private readonly IConfigurationProvider mapper;

		public MechanicService(QueryService2DbContext data)
			=> this.data = data;

		public MechanicService(QueryService2DbContext data, IMapper mapper)
		{
			this.data = data;
			this.mapper = mapper.ConfigurationProvider;
		}

		private IEnumerable<MechanicServiceModel> GetMechanics(IQueryable<Mechanic> mechanic)
			=> mechanic
			.ProjectTo<MechanicServiceModel>(this.mapper)
			.ToList();

		public CarQueryServiceModel AllMechanics(string name, string code, string phoneNumebr)
		{
			var q = this.data.Mechanics
				.Where(m => m.Id != 0);

			if (!string.IsNullOrWhiteSpace(name))
			{
				q = q.Where(c => c.Name == name);
			}

			var totalMechanics = q.Count();
			var mechanics = GetMechanics(q);

			return new CarQueryServiceModel
			{
				TotalMechanics = totalMechanics,
				Mechanics = mechanics
			};
		}

		public int IdByUser(string userId)
			=> this.data
			.Mechanics
				.Where(d => d.UserId == userId)
				.Select(d => d.Id)
				.FirstOrDefault();


		public bool IsMechanic(string userId)
			=> this.data
			.Mechanics
			.Any(d => d.UserId == userId);

		public MechanicDetailsServiceModel Details(int id)
		=> this.data
		.Mechanics
		.Where(c => c.Id == id)
		.ProjectTo<MechanicDetailsServiceModel>(this.mapper)
		.FirstOrDefault();

		public bool Edit(
			int id,
			string name,
			string code,
			string phoneNumber)
		{
			var mechanicData = this.data.Mechanics.Find(id);

			if (mechanicData == null)
			{
				return false;
			}

			mechanicData.Id = id;
			mechanicData.Name = name;
			mechanicData.Code = code;
			mechanicData.PhoneNumber = phoneNumber;

			this.data.SaveChanges();

			return true;
		}
	}
}
