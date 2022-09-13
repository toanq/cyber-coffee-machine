using System.Text.Json.Nodes;
using System.Web;

namespace Api.Client
{
    public class OpenWeatherClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _openWeatherCfg;
        public OpenWeatherClient(
            HttpClient client,
            IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
            _openWeatherCfg = _configuration.GetSection("OpenWeather");
        }

        public async Task<double> GetCurrentTemperature()
        {
            var uriBuilder = new UriBuilder("https://api.openweathermap.org/data/2.5/weather");
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["lat"] = _openWeatherCfg.GetValue<string>("Latitude");
            parameters["lon"] = _openWeatherCfg.GetValue<string>("Longitude");
            parameters["appid"] = _openWeatherCfg.GetValue<string>("AppId");
            parameters["units"] = _openWeatherCfg.GetValue<string>("Units");

            uriBuilder.Query = parameters.ToString();
            var uri = uriBuilder.Uri;

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
    }
}
