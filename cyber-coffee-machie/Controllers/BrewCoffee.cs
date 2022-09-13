using cyber_coffee_machie.Models;
using Microsoft.AspNetCore.Mvc;

namespace cyber_coffee_machie.Controllers
{
    [ApiController]
    public class BrewCoffee : ControllerBase
    {
        private readonly ILogger<BrewCoffee> _logger;

        public BrewCoffee(ILogger<BrewCoffee> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("brew-coffee")]
        public BrewCoffeeResponse RequestBrewCoffee()
        {
            return new BrewCoffeeResponse()
            {
                Message = "Your piping hot coffee is ready",
                Prepared = DateTimeOffset.Now
            };
        }
    }
}
