using QueryServiceSystem2.Data.Models;
using System;

namespace QueryServiceSystem2.Services.Queries.Models
{
	public class LatestQueryServiceModel : IQueryModel
	{
		public int Id { get; set; }
		public string CarModel { get; set; }
		public string CarRegistrationNumber { get; set; }
		public string CarColor { get; set; }
		public string ImageUrl { get; set; }

		public string Description { get; set; }

		public DateTime Date { get; set; }

		public Query Query { get; set; }

		public Car Car { get; set; }

		public string Brand { get; set; }
	}
}
