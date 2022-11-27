namespace QueryServiceSystem2.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Car;

    public class Car
    {
        public int Id { get; init; }

        [Required(ErrorMessage = "Марката на автомобил е задължителна")]
        public string Name { get; set; }


        public IEnumerable<Query> Queries { get; init; } = new List<Query>();
    }
}
