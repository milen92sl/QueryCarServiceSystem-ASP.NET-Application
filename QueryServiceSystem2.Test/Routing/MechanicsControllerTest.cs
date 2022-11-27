namespace QueryServiceSystem2.Test.Routing
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using QueryServiceSystem2.Controllers;
    using QueryServiceSystem2.Models.Mechanics;

    public class MechanicsControllerTest
    {
        [Fact]
        public void RoutTest()
        => MyRouting
            .Configuration()
            .ShouldMap("/Mechanics/Become")
            .To<MechanicsController>(c => c.Become())
            .Which()
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();

        [Fact]
        public void GetBecomeShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Mechanics/Become")
            .To<MechanicsController>(c => c.Become());

        [Fact]
        public void PostBecomeShouldBeMapped()
           => MyRouting
           .Configuration()
           .ShouldMap(request => request
           .WithPath("/Mechanics/Become")
           .WithMethod(HttpMethod.Post))
           .To<MechanicsController>(c => c.Become(With.Any<BecomeMechanicFormModel>()));
    }
}
