namespace QueryServiceSystem2.Test.Pipeline
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using QueryServiceSystem2.Controllers;
    using QueryServiceSystem2.Models.Mechanics;
    using System.Linq;


    using static WebConstants;
    using QueryServiceSystem2.Data.Models;

    public class MechanicsControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeForAuthorizedUsersAndReturnView()
                    => MyPipeline
                    .Configuration()
                    .ShouldMap(request => request
                    .WithPath("/Mechanics/Become")
                    .WithUser())
                    .To<MechanicsController>(c => c.Become())
                    .Which()
                    .ShouldHave()
                    .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                    .AndAlso()
                    .ShouldReturn()
                    .View();

        [Theory]
        [InlineData("Mechanic", "Code", "+35988888888")]
        public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            string MechanicName,
            string MechanicCode,
            string phoneNumber)
            => MyPipeline
            .Configuration()
            .ShouldMap(request => request
            .WithPath("/Mechanics/Become")
            .WithMethod(HttpMethod.Post)
            .WithFormFields(new
            {
                Name = MechanicName,
                Code = MechanicCode,
                PhoneNumber = phoneNumber
            })
            .WithUser()
            .WithAntiForgeryToken())
            .To<MechanicsController>(c => c.Become(new BecomeMechanicFormModel
            {
                Name = MechanicName,
                Code = MechanicCode,
                PhoneNumber = phoneNumber,
            }))
            .Which()
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForHttpMethod(HttpMethod.Post)
            .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data.WithSet<Mechanic>(Mechanics => Mechanics
            .Any(d =>
                d.Name == MechanicName &&
                d.Code == MechanicCode &&
                d.PhoneNumber == phoneNumber &&
                d.UserId == TestUser.Identifier)))
            .TempData(tempData => tempData
            .ContainingEntryWithKey(GlobalMessageKey))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("All", "Queries");
    }
}
