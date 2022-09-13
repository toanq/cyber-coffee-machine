using System.Text.Json.Nodes;
using System.Web;

namespace Api.Client
{
    public class OpenWeatherClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        public OpenWeatherClient(
            HttpClient client,
            IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<double> GetCurrentTemperature()
        {
            var uri = GetWeatherUri();

            var response = await _client.GetAsync(uri);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var temperature = JsonNode.Parse(content)?["main"]?["temp"]?.GetValue<double>();

            if (temperature is null)
            {
                throw new NullReferenceException("Cannot parse temperature from response message");
            }
            else
            {
                return (double)temperature;
            }

        }

        private Uri GetWeatherUri()
        {
            var config = _configuration.GetSection("OpenWeather");

            var uriBuilder = new UriBuilder("https://api.openweathermap.org/data/2.5/weather");

            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["lat"] = config.GetValue<string>("Latitude");
            parameters["lon"] = config.GetValue<string>("Longitude");
            parameters["appid"] = config.GetValue<string>("AppId");
            parameters["units"] = config.GetValue<string>("Units");

            uriBuilder.Query = parameters.ToString();

            return uriBuilder.Uri;
        }
    }
}
