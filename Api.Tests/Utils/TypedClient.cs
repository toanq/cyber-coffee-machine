using Api.Client;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;

namespace Api.Tests.Utils
{
    public static class TypedClient
    {
        public static OpenWeatherClient GetFakeTypedClient(string response)
        {
            var httpResponse = new HttpResponseMessage()
            {
                Content = new StringContent(response)
            };

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);

            var cfgBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var cfg = cfgBuilder.Build();

            return new OpenWeatherClient(httpClient, cfg);
        }
    }
}
