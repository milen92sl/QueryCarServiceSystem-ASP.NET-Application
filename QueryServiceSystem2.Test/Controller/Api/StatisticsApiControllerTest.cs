namespace QueryServiceSystem2.Test.Controller.Api
{
    using QueryServiceSystem2.Controllers.Api;
    using QueryServiceSystem2.Test.Moq;
    using Xunit;

    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            //Arrange
            var statisticsController = new StatisticsApiController(StatisticsServiceMock.Instance);

            //Act
            var result = statisticsController.GetStatistics();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(5,result.TotalQueries);
            Assert.Equal(10, result.TotalQueriesOnWaiting);
            Assert.Equal(15,result.TotalUsers);
        }
    }
}
