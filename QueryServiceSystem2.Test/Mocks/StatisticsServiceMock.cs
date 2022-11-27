namespace QueryServiceSystem2.Test.Moq
{
    using QueryServiceSystem2.Services.Statistics;
    using global::Moq;

    public static class StatisticsServiceMock
    {
        public static IStatisticsService Instance
        {
            get
            {
               var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock
                    .Setup(s=>s.Total())
                    .Returns(new StatisticsServiceModel
                    {
                        TotalQueries = 5,
                        TotalQueriesOnWaiting = 10, 
                        TotalUsers = 15
                    });

                return statisticsServiceMock.Object;
            }
        }
    }
}
