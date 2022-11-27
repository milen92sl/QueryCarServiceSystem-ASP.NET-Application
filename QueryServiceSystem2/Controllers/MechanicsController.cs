namespace QueryServiceSystem2.Controllers
{
    using QueryServiceSystem2.Data;
    using QueryServiceSystem2.Data.Models;
    using QueryServiceSystem2.Infrastructure.Extensions;
    using QueryServiceSystem2.Models.Mechanics;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using static WebConstants;
	using QueryServiceSystem2.Services.Mechanics;
	using AutoMapper;

	public class MechanicsController : Controller
    {
        private readonly QueryService2DbContext data;
        private readonly IMechanicService mechanics;
        private readonly IMapper mapper;
		public MechanicsController(QueryService2DbContext data, IMapper mapper, IMechanicService mechanics)
		{
			this.data = data;
			this.mapper = mapper;
			this.mechanics = mechanics;
		}

		[Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeMechanicFormModel Mechanic)
        {
            var userId = this.User.Id();

            var userIdAlreadyMechanic = this.data
                .Mechanics
                .Any(d => d.UserId == userId);

            if (userIdAlreadyMechanic)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(Mechanic);
            }

            var MechanicData = new Mechanic
            {
                Name = Mechanic.Name,
                Code = Mechanic.Code,
                PhoneNumber = Mechanic.PhoneNumber,
                UserId = userId
            };

            this.data.Mechanics.Add(MechanicData);
            this.data.SaveChanges();

            TempData[GlobalMessageKey] = "Благодарим ви, че се регистрирахте като механик!";

            return RedirectToAction(nameof(QueriesController.All), "Queries");
        }
        public IActionResult AllMechanics()
        {
            var Mechanics = this.mechanics
                .AllMechanics()
                .Mechanics;

            return View(Mechanics);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var mechanic = this.mechanics.Details(id);
            var mechanicForm = this.mapper.Map<MechanicFormModel>(mechanic);

            return View(mechanicForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, MechanicFormModel model)
        {

            var edited = this.mechanics.Edit(
                model.Id,
                model.Name,
                model.Code,
                model.PhoneNumber);

            if (!edited)
            {
                return BadRequest();
            }

            TempData[GlobalMessageKey] = $"Потребителят/механикът беше редактиран успешно {(this.User.IsAdmin() ? string.Empty : " и ще бъде одобрен след като бъде прегледан от управител")}!";

            return RedirectToAction(nameof(Edit));
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Mechanic mecahnic = data.Mechanics.Find(id);

            if (mecahnic == null)
            {
                return NotFound();
            }
            return View(mecahnic);
        }

        // POST: /Queries/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mechanic mechanic = data.Mechanics.Find(id);
            data.Mechanics.Remove(mechanic);
            
            data.SaveChanges(acceptAllChangesOnSuccess: true);
            return RedirectToAction(nameof(AllMechanics));
        }
    }
}
