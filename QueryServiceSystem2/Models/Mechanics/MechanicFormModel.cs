using QueryServiceSystem2.Services.Mechanics.Models;
using QueryServiceSystem2.Services.Queries.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static QueryServiceSystem2.Data.DataConstants.Query;

namespace QueryServiceSystem2.Models.Mechanics
{
	public class MechanicFormModel : IMechanicModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public string PhoneNumber { get; set; }
	}
}
