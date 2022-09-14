using Api.Tests.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Api.Client.Tests
{
    [TestClass]
    public class OpenWeatherClientTests
    {
        [TestMethod]
        public async Task GetCurrentTemperatureReturnDoubleIfNotError()
        {
            // Arrange
            var client = TypedClient.GetFakeTypedClient("{\"main\":{\"temp\":30.3030}}");

            // Act
            var temperature = await client.GetCurrentTemperature();

            // Assert
            Assert.AreEqual(temperature, 30.3030);
        }

        [TestMethod]
        public async Task GetCurrentTemperatureThrowExceptionIfCannotParseResponse()
        {
            // Arrange
            var client = TypedClient.GetFakeTypedClient("{}");

            // Act

            // Assert
            await Assert.ThrowsExceptionAsync<NullReferenceException>(async () =>
            {
                await client.GetCurrentTemperature();
            });
        }
    }
}