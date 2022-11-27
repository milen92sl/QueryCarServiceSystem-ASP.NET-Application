﻿namespace QueryServiceSystem2.Test.Controller
{
    using Microsoft.AspNetCore.Mvc.Testing;
    using System.Threading.Tasks;
    using Xunit;
    public class HomeControllerSystemTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public HomeControllerSystemTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task IndexShouldReturnCorrectStatusCode()
        {
            // Arrange
            var client =  this.factory.CreateClient();

            // Act
            var result =  await client.GetAsync("/");

            // Assert
            Assert.True(result.IsSuccessStatusCode);            
        }
    }
}
