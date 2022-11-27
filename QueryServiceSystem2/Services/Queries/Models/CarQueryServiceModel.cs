namespace QueryServiceSystem2.Services.Queries.Models
{
	using QueryServiceSystem2.Services.Mechanics.Models;
	using System.Collections.Generic;

    public class CarQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int QueriesPerPage { get; init; }

        public int TotalQueries { get; set; }
        public int TotalMechanics { get; set; }

        public IEnumerable<QueryServiceModel> Queries { get; init; }
        public IEnumerable<MechanicServiceModel> Mechanics { get; init; }
    }
}
