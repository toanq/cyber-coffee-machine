using Api.Extensions;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace cyber_coffee_machie.Controllers
{
    [ApiController]
    public class BrewCoffee : ControllerBase
    {
        private readonly ILogger<BrewCoffee> _logger;
        private readonly ICoffeeCountService _count;
        public BrewCoffee(
            ILogger<BrewCoffee> logger,
            ICoffeeCountService count
        )
        {
            _logger = logger;
            _count = count;
        }

        [HttpGet]
        [Route("brew-coffee")]
        public IActionResult RequestBrewCoffee()
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

            _logger.LogInformation("Prepare a coffe, current count is {0}", _count.Value);
            _count.Increase();

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
