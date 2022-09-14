using Microsoft.VisualStudio.TestTools.UnitTesting;
using Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Api.Services;
using Api.Tests.Utils;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace Api.Controllers.Tests
{
    [TestClass]
    public class BrewCoffeeTests
    {
        private readonly ICoffeeCountService _count;
        private readonly Mock<ILogger<BrewCoffee>> _logger;

        public BrewCoffeeTests()
        {
            _logger = new Mock<ILogger<BrewCoffee>>();
            _count = new CoffeeCountService();
        }

        [TestMethod]
        public async Task RequestBrewCoffeeShouldServeIcedCoffeeIfTemperatureGreater30()
        {
            // Arrange
            var client = TypedClient.GetFakeTypedClient("{\"main\":{\"temp\":30.3030}}");
            var coffeeController = new BrewCoffee(_logger.Object, _count, client);

            // Act
            var response = (OkObjectResult) await coffeeController.RequestBrewCoffee();
            var value = (BrewCoffeeResponse?) response.Value;

            // Assert
            Assert.AreEqual(response.StatusCode, StatusCodes.Status200OK);

            Assert.IsNotNull(value);
            Assert.AreEqual(value.Message, "Your refreshing iced coffee is ready");
        }

        [TestMethod]
        public async Task RequestBrewCoffeeShouldServeHotCoffeeIfTemperatureBellow30()
        {
            // Arrange
            var client = TypedClient.GetFakeTypedClient("{\"main\":{\"temp\":29}}");
            var coffeeController = new BrewCoffee(_logger.Object, _count, client);

            // Act
            var response = (OkObjectResult)await coffeeController.RequestBrewCoffee();
            var value = (BrewCoffeeResponse?)response.Value;

            // Assert
            Assert.AreEqual(response.StatusCode, StatusCodes.Status200OK);

            Assert.IsNotNull(value);
            Assert.AreEqual(value.Message, "Your piping hot coffee is ready");
        }

        [TestMethod]
        public async Task RequestBrewCoffeeShouldStopServeEach5thCall()
        {
            // Arrange
            var client = TypedClient.GetFakeTypedClient("{\"main\":{\"temp\":29}}");
            var coffeeController = new BrewCoffee(_logger.Object, _count, client);

            // Act
            for (var i = 0; i < 4; i++)
                await coffeeController.RequestBrewCoffee();

            var response = (ObjectResult) await coffeeController.RequestBrewCoffee();

            // Assert
            Assert.AreEqual(response.StatusCode, StatusCodes.Status503ServiceUnavailable);
            Assert.AreEqual(response.Value, string.Empty);
        }
    }
}