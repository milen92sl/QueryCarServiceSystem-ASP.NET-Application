using QueryServiceSystem2.Data.Models;

namespace QueryServiceSystem2.Services.Mechanics.Models
{
	public class MechanicServiceModel : IMechanicModel
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string PhoneNumber { get; set; }
		public Mechanic Mechanic { get; set; }
    }
}
