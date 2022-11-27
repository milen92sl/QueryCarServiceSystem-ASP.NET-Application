namespace QueryServiceSystem2.Test.Services
{
    using QueryServiceSystem2.Data.Models;
    using QueryServiceSystem2.Services.Mechanics;
    using QueryServiceSystem2.Test.Mocks;
    using Xunit;
    public class MechanicServiceTest
    {
        private const string userId = "TestUserId";
        [Fact]
        public void IsMechanicShouldReturnTrueWhenUserIsMechanic()
        {
            //Arrange
            var MechanicService = GetMechanicService();

            //Act
            var result = MechanicService.IsMechanic(userId);

            //Assert    
            Assert.True(result);
        }

        [Fact]
        public void IsMechanicShouldReturnFalseWhenUserIsNotMechanic()
        {
            //Arrange
            var MechanicService = GetMechanicService();

            //Act
            var result = MechanicService.IsMechanic("AnotherUserId");

            //Assert    
            Assert.False(result);
        }

        private static IMechanicService GetMechanicService()
        {
            var data = DatabaseMock.Instance;

            data.Mechanics.Add(new Mechanic { UserId = userId });
            data.SaveChanges();

            return new MechanicService(data);

        }
    }
}
