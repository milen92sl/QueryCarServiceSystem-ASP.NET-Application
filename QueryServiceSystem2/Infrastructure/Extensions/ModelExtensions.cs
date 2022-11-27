namespace QueryServiceSystem2.Infrastructure.Extensions
{
    using QueryServiceSystem2.Services.Queries.Models;

    public static class ModelExtensions
    {
        public static string GetInformation(this IQueryModel query)
        => query.Brand + "-" + @query.Date;
    }
}
