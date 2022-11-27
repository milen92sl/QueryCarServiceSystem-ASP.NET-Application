using QueryServiceSystem2.Data.Models;
using System;

namespace QueryServiceSystem2.Services.Queries.Models
{
    public class QueryServiceModel : IQueryModel
    {
        public int Id { get; init; }

        public string ImageUrl { get; init; }

        public string Brand { get; init; }

        public string CarName { get; init; }

        public string CarModel { get; set; }

        public string CarRegistrationNumber { get; set; }

        public string CarColor { get; set; }

        public string Description { get; init; }

        public bool IsPublic { get; init; }

		public DateTime Date { get; set; }

		public Query Query { get; set; }
    }
}
