namespace QueryServiceSystem2.Controllers.Api
{
    using QueryServiceSystem2.Models.Api.Queries;
    using QueryServiceSystem2.Services.Queries;
    using QueryServiceSystem2.Services.Queries.Models;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/Queries")]
    public class QueriesApiController : ControllerBase
    {
        private readonly IQueryService Queries;

        public QueriesApiController(IQueryService Queries) => 
            this.Queries = Queries;

        [HttpGet]
        public CarQueryServiceModel All([FromQuery] AllQueriesApiRequestModel query) 
            => this.Queries.All(
               query.Brand,
               query.CurrentPage,
               query.QueriesPerPage);

        [HttpGet]
        public CarQueryServiceModel AllReferences([FromQuery] AllQueriesApiRequestModel query)
           => this.Queries.AllReferences(
              query.Brand,
              query.SearchTerm,
              query.Sorting,
              query.CurrentPage,
              query.QueriesPerPage);

    }
}
