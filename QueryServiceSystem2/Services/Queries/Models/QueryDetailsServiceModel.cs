namespace QueryServiceSystem2.Services.Queries.Models
{
    public class QueryDetailsServiceModel : QueryServiceModel
    {
        public int CarId { get; init; }

        public int MechanicId { get; init; }

        public string MechanicName { get; init; }

        public string UserId { get; init; }
    }
}
