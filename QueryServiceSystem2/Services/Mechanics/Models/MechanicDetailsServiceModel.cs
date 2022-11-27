namespace QueryServiceSystem2.Services.Mechanics.Models
{
    public class MechanicDetailsServiceModel : MechanicServiceModel
    {
        public int MechanicId { get; init; }
        public string MechanicName { get; set; }
        public string MechanicCode { get; set; }
        public string MechanicPhoneNumber { get; set; }
    }
}
