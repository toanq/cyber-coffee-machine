using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Api.Services.Tests
{
    [TestClass()]
    public class CoffeeCountServiceTests
    {
        [TestMethod]
        public void IncreaseTest()
        {
            // Arrange
            var service = new CoffeeCountService();

            // Action
            service.Increase();

            // Assert
            Assert.AreEqual(service.Value, 1);
        }

        [TestMethod]
        public void ResetTest()
        {
            // Arrange
            var service = new CoffeeCountService();

            // Action
            service.Increase();
            service.Reset();

            // Assert
            Assert.AreEqual(service.Value, 0);
        }
    }
}