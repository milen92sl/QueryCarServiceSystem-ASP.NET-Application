using Xunit;
using MyTested.AspNetCore.Mvc;
using QueryServiceSystem2.Controllers;
using QueryServiceSystem2.Models.Mechanics;
using QueryServiceSystem2.Data.Models;
using System.Linq;

using static QueryServiceSystem2.WebConstants;

namespace QueryServiceSystem2.Test.Controller
{
	public class MechanicsControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeForAuthorizedUsersAndReturnView()
            => MyController<MechanicsController>
            .Instance()
            .Calling(c => c.Become())
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();

        [Theory]
        [InlineData("Mechanic","Code", "+35988888888")]
        public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            string MechanicName,
            string MechanicCode,
            string phoneNumber)
            => MyController<MechanicsController>
            .Instance(controller => controller
            .WithUser())
            .Calling(c => c.Become(new BecomeMechanicFormModel
            {
                Name = MechanicName,
                Code = MechanicCode,
                PhoneNumber = phoneNumber,
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForHttpMethod(HttpMethod.Post)
            .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data.WithSet<Mechanic>(Mechanics => Mechanics
            .Any(d =>
                d.Name == MechanicName &&
                d.PhoneNumber == phoneNumber &&
                d.UserId == TestUser.Identifier)))
            .TempData(tempData=> tempData
            .ContainingEntryWithKey(GlobalMessageKey))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("All","Queries");
    }
}
