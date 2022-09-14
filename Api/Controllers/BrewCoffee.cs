using Api.Client;
using Api.Extensions;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class BrewCoffee : ControllerBase
    {
        private readonly double LIMIT_TEMPERATURE = 30.0;

        private readonly ILogger<BrewCoffee> _logger;
        private readonly ICoffeeCountService _count;
        private readonly OpenWeatherClient _client;
        public BrewCoffee(
            ILogger<BrewCoffee> logger,
            ICoffeeCountService count,
            OpenWeatherClient client
        )
        {
            _logger = logger;
            _count = count;
            _client = client;
        }

        [HttpGet]
        [Route("brew-coffee")]
        public async Task<IActionResult> RequestBrewCoffee()
        {
            if (DateTimeOffset.Now.IsAprilFools())
                return HttpStatusCodeResult(StatusCodes.Status418ImATeapot);

            var response = new BrewCoffeeResponse()
            {
                Message = "Your piping hot coffee is ready",
                Prepared = DateTimeOffset.Now
            };

            // _count start at 0 so 5th call is 4
            if (_count.Value == 4)
            {
                _logger.LogInformation("5th call, reset the count", _count.Value);
                _count.Reset();

                return HttpStatusCodeResult(StatusCodes.Status503ServiceUnavailable);
            }

            _logger.LogInformation("Prepare a coffe, current count is {value}", _count.Value);
            _count.Increase();

            if (await _client.GetCurrentTemperature() > LIMIT_TEMPERATURE)
            {
                response.Message = "Your refreshing iced coffee is ready";
            }

            return Ok(response);
        }

        private static IActionResult HttpStatusCodeResult(int statusCode)
        {
            return new ObjectResult("")
            {
                StatusCode = statusCode
            };
        }
    }
}
