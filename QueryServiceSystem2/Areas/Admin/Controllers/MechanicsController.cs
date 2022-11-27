namespace QueryServiceSystem2.Areas.Admin.Controllers
{
    using QueryServiceSystem2.Services.Mechanics;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public class MechanicsController : AdminController
    {
        private readonly IMechanicService Mechanics;
        public MechanicsController(IMechanicService Mechanics) => this.Mechanics = Mechanics;

        public IActionResult AllMechanics()
        {
            var Mechanics = this.Mechanics
                .AllMechanics()
                .Mechanics;

            return View(Mechanics);
        }

    }
}
