using QueryServiceSystem2.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using MyTested.AspNetCore.Mvc;

namespace QueryServiceSystem2.Test.Controller
{
	public class HomeControllerTest
	{
		[Fact]
		public void ErrorShouldReturnView()
		{
			//Arrange 
			var homeController = new HomeController(null, null);

			//Act
			var result = homeController.Error();

			//Assert
			Assert.NotNull(result);
			Assert.IsType<ViewResult>(result);
		}

		[Fact]
		public void ErrorShouldReturnView2()
		=> MyMvc
			.Pipeline()
			.ShouldMap("/Home/Error")
			.To<HomeController>(c => c.Error())
			.Which()
			.ShouldReturn()
			.View();


		[Fact]
		public void ErrorShouldReturnView3()
		 => MyController<HomeController>
		 .Instance()
		 .Calling(c => c.Error())
		 .ShouldReturn()
		 .View();


	}
}
