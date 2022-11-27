namespace QueryServiceSystem2.Services.Queries
{
	using QueryServiceSystem2.Models;
	using QueryServiceSystem2.Services.Queries.Models;
	using System;
	using System.Collections.Generic;

	public interface IQueryService
	{
		CarQueryServiceModel All(
			string brand = null,
				int currentPage = 1,
				int queriesPerPage = int.MaxValue,
				bool publicOnly = true);

		CarQueryServiceModel AllReferences(
		string brand = null,
		string searchTerm = null,
		QueriesSorting sorting = QueriesSorting.Date,
		int currentPage = 1,
		int queriesPerPage = int.MaxValue,
		bool publicOnly = true);

		IEnumerable<LatestQueryServiceModel> Latest();
		QueryDetailsServiceModel Details(int queryId);

		int Create(
			string brand,
			string carModel,
			string carRegistrationNumber,
			string carColor,
			string description,
			string imageUrl,
			DateTime date,
			int carId,
			int mechanicId);

		bool Edit(
			int id,
			string brand,
			string carModel,
			string carRegistrationNumber,
			string carColor,
			string description,
			string imageUrl,
			DateTime date,
			int carId,
			bool isPublic);

		IEnumerable<QueryServiceModel> ByUser(string userId);

		bool IsByMechanic(int queryId, int dealerId);

		void ChangeVisibility(int queryId);
		IEnumerable<string> AllBrands();

		IEnumerable<QueryCarServiceModel> AllCars();

		bool CarExists(int CarId);
	}
}
