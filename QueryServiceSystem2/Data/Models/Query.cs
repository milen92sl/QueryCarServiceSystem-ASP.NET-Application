namespace QueryServiceSystem2.Data.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;
    using static DataConstants.Query;

    public class Query
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        [Required(ErrorMessage = "Вписването на модел на автомобила е задължително")]
        public string Model { get; set; }
        public string RegistrationNumber { get; set; }

        [Required(ErrorMessage = "Вписването на цвят е задължително")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Датата е задължителна")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Описанието на проблема е задължително")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Прикачен URL към автомобила е задължителен")]
        public string ImageUrl { get; set; }

        public bool IsPublic { get; set; }

        public int CarId { get; set; }

        public Car Car { get; init; }

        public int MechanicId { get; init; }

        public Mechanic Mechanic { get; init; }
    }
}
