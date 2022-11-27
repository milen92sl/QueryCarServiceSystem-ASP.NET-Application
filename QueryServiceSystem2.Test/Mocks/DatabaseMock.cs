using QueryServiceSystem2.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace QueryServiceSystem2.Test.Mocks
{


    public static class DatabaseMock
    {
        public static QueryService2DbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<QueryService2DbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new QueryService2DbContext(dbContextOptions);
            }

        }
    }
}
