namespace QueryServiceSystem2.Areas.Admin.Controllers
{
    using QueryServiceSystem2.Services.Queries;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public class QueriesController : AdminController
    {
        private readonly IQueryService Queries;

        public QueriesController(IQueryService Queries) => this.Queries = Queries;

        public IActionResult All()
        {
            var Queries = this.Queries
                .All(publicOnly: false)
                .Queries;

            return View(Queries);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.Queries.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
