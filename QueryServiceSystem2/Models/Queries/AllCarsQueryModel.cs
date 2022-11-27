using QueryServiceSystem2.Services.Mechanics.Models;
using QueryServiceSystem2.Services.Queries.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QueryServiceSystem2.Models.Queries
{
	public class AllCarsQueryModel
    {
        public const int QueriesPerPage = 6;

        public string Brand { get; set; }

        public string SearchTerm { get; init; }

        public QueriesSorting Sorting { get; init; }
        
        public int CurrentPage { get; init; } = 1;

        public int TotalQueries { get; set; }

        public IEnumerable<string> Brands { get; set; }
        public IEnumerable<QueryServiceModel> Queries { get; set; }
        public IEnumerable<MechanicServiceModel> Mechanics { get; set; }
    }
}
