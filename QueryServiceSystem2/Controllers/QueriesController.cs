using AutoMapper;
using QueryServiceSystem2.Data;
using QueryServiceSystem2.Data.Models;
using QueryServiceSystem2.Infrastructure.Extensions;
using QueryServiceSystem2.Models.Queries;
using QueryServiceSystem2.Services.Queries;
using QueryServiceSystem2.Services.Mechanics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static QueryServiceSystem2.WebConstants;
using System.Collections.Generic;
using QueryServiceSystem2.Services.Queries.Models;
using System.Linq;

namespace QueryServiceSystem2.Controllers
{
	public class QueriesController : Controller
    {
        private readonly IQueryService queries;
        private readonly IMechanicService mechanics;
        private readonly QueryService2DbContext data;
        private readonly IMapper mapper;


        public QueriesController(
            QueryService2DbContext data,
            IQueryService queries,
            IMechanicService mechanics,
            IMapper mapper)
        {
            this.data = data;
            this.queries = queries;
            this.mechanics = mechanics;
            this.mapper = mapper;
        }



        [Authorize]
        public IActionResult Mine()
        {
            var myQueries = this.queries.ByUser(this.User.Id());

            return View(myQueries);
        }

        public IActionResult Details(int id, string information)
        {
            var model = this.queries.Details(id);

            if (information != model.GetInformation())
            {
                return BadRequest();
            }

            return View(model);
        }
        [Authorize]
        public IActionResult Add()
        {
            if (!this.mechanics.IsMechanic(this.User.Id()))
            {
                return RedirectToAction(nameof(MechanicsController.Become), "Mechanics");
            }

            return View(new QueryFormModel
            {
                Cars = this.queries.AllCars()
            });
        }

        [HttpPost]
        [Authorize]
		public IActionResult Add(QueryFormModel model)
		{
            var mechanicId = this.mechanics.IdByUser(this.User.Id());
            var currentQueryIdNumber = model.Id;

            if (mechanicId == 0)
            {
                return RedirectToAction(nameof(MechanicsController.Become), "Mechanics");
            }
			if (queries.All().Queries.Any(x => x.Id == currentQueryIdNumber))
			{
                model.Id++;
                data.SaveChanges();
			}

            if (!this.queries.CarExists(model.CarId))
            {
                this.ModelState.AddModelError(nameof(model.CarId), "Този автомобил не съществува!");
            }

            if (!ModelState.IsValid)
            {
                model.Cars = this.queries.AllCars();

                return View(model);
            }

            var queryId = this.queries.Create(
                model.Brand,
                model.CarModel,
                model.CarRegistrationNumber,
                model.CarColor,
                model.Description,
                model.ImageUrl,
                model.Date,
                model.CarId,
                mechanicId);

            TempData[GlobalMessageKey] = "Вашата заявка беше записана успешно ще бъде визуализирана след одобрение на управител!";

            return RedirectToAction(nameof(Details), new { id = queryId, information = model.GetInformation() });
        }

        public IActionResult All([FromQuery] AllCarsQueryModel query)
        {
            var queryResult = this.queries.All(
                query.Brand,
                query.CurrentPage,
                AllCarsQueryModel.QueriesPerPage);

            var queryBrands = this.queries.AllBrands();

            query.Brands = queryBrands;
            query.TotalQueries = queryResult.TotalQueries;
            query.Queries = queryResult.Queries;

            return View(query);
        }

        public IActionResult AllReferences([FromQuery] AllCarsQueryModel query)
        {
            var queryResult = this.queries.AllReferences(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllCarsQueryModel.QueriesPerPage);

            var queryBrands = this.queries.AllBrands();

            query.Brands = queryBrands;
            query.TotalQueries = queryResult.TotalQueries;
            query.Queries = queryResult.Queries;

            return View(query);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.mechanics.IsMechanic(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(MechanicsController.Become), "Mechanics");
            }

            var Query = this.queries.Details(id);

            if (Query.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var QueryForm = this.mapper.Map<QueryFormModel>(Query);

            QueryForm.Cars = this.queries.AllCars();

            return View(QueryForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, QueryFormModel model)
        {
            var mechanicId = this.mechanics.IdByUser(this.User.Id());

            if (mechanicId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(MechanicsController.Become), "Mechanics");
            }

            if (!this.queries.CarExists(model.CarId))
            {
                this.ModelState.AddModelError(nameof(model.CarId), "Този автомобил не съществува");
            }

            if (!ModelState.IsValid)
            {
                model.Cars = this.queries.AllCars();

                return View(model);
            }

            if (!this.queries.IsByMechanic(id, mechanicId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            var edited = this.queries.Edit(
                model.Id,
                model.Brand,
                model.CarModel,
                model.CarRegistrationNumber,
                model.CarColor,
                model.Description,
                model.ImageUrl,
                model.Date,
                model.CarId,
                this.User.IsAdmin());

            if (!edited)
            {
                return BadRequest();
            }

            TempData[GlobalMessageKey] = $"Заявката беше редактирана успешно {(this.User.IsAdmin() ? string.Empty : " и ще бъде видима след като бъде прегледана от управител")}!";

            return RedirectToAction(nameof(Details), new { id, information = model.GetInformation() });
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Query Query = data.Queries.Find(id);

            if (Query == null)
            {
                return NotFound();
            }
            return View(Query);
        }

        // POST: /Queries/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Query Query = data.Queries.Find(id);
            data.Queries.Remove(Query);
            data.SaveChanges();
            return RedirectToAction(nameof(All));
        }
    }
}
