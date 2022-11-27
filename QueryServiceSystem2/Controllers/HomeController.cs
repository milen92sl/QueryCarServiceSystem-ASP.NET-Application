namespace QueryServiceSystem2.Controllers
{
    using QueryServiceSystem2.Services.Queries;
    using QueryServiceSystem2.Services.Queries.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using static WebConstants.Cache;
    public class HomeController : Controller
    {
        private readonly IQueryService Queries;
        private readonly IMemoryCache cache;

        public HomeController(
            IQueryService Queries,
            IMemoryCache cache)
        {

            this.Queries = Queries;
            this.cache = cache;
        }

        public IActionResult Index()
        {

            var latestQueries = this.cache.Get<List<LatestQueryServiceModel>>(LatestQueriesCacheKey);

            if (latestQueries == null)
            {
                latestQueries = this.Queries
                    .Latest()
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                this.cache.Set(LatestQueriesCacheKey, latestQueries, cacheOptions);
            }

            return View(latestQueries);
        }

        public IActionResult Error() => View();
    }
}
