namespace QueryServiceSystem2.Test.Data
{
    using QueryServiceSystem2.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class Queries
    {
        public static IEnumerable<Query> TenPublicQueries()
        => Enumerable.Range(0, 10).Select(i => new Query
        {
            IsPublic = true
        });

    }
}
