namespace QueryServiceSystem2.Models.Api.Queries
{
    using QueryServiceSystem2.Models;

    public class AllQueriesApiRequestModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }

        public string SearchTerm { get; init; }

        public QueriesSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int QueriesPerPage { get; init; } = 10;

    }
}
