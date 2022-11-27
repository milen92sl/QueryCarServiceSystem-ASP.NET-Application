namespace QueryServiceSystem2.Models.Queries
{
	using Microsoft.VisualStudio.TestPlatform.CoreUtilities.Extensions;
	using QueryServiceSystem2.Data.Models;
	using QueryServiceSystem2.Services.Queries.Models;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using static Data.DataConstants.Query;

	public class QueryFormModel : IQueryModel
	{
		public QueryFormModel()
		{
			Date = DateTime.Now;
		}

		[ReadOnly(true)]
		public int Id { get; set; }

		[Required(ErrorMessage = "Датата е задължителна")]
		public DateTime Date { get; set; }

		[Required(ErrorMessage = "Описанието на проблема е задължително")]
		[StringLength(int.MaxValue, MinimumLength = DescriptionMinLength, ErrorMessage = "Описанието на проблема трябва да съдържа минимум {2} символа")]
		public string Description { get; init; }

		[Url(ErrorMessage = "Моля въведете правилен URL адрес")]
		[Required(ErrorMessage = "Прикачен URL към автомобила е задължителен")]
		public string ImageUrl { get; init; }

		public int CarId { get; init; }

		[Required(ErrorMessage = "Вписването на модел на автомобила е задължително")]
		public string CarModel { get; init; }

		[Required(ErrorMessage = "Вписването на цвят е задължително")]
		public string CarColor { get; init; }

		public string CarRegistrationNumber { get; init; }

		public IEnumerable<QueryCarServiceModel> Cars { get; set; }

		public string Brand { get; set; }
	}
}
