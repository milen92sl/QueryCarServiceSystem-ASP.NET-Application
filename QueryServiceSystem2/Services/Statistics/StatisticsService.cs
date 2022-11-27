namespace QueryServiceSystem2.Services.Statistics
{
    using QueryServiceSystem2.Data;
    using System.Linq;

    public class StatisticsService : IStatisticsService
    {
        private readonly QueryService2DbContext data;

        public StatisticsService(QueryService2DbContext data) 
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalQueries = this.data.Queries.Count(q => q.IsPublic);
            var totalUsers = this.data.Users.Count();
            var totalQueriesOnWaiting = this.data.Queries.Count(q => !q.IsPublic);

            return new StatisticsServiceModel
            {
                TotalQueries = totalQueries,
                TotalUsers = totalUsers,
                TotalQueriesOnWaiting = totalQueriesOnWaiting
            };
        }
    }
}
