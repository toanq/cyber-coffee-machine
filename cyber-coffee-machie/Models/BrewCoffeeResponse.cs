namespace cyber_coffee_machie.Models
{
    public class BrewCoffeeResponse
    {
        public string Message { get; set; } = string.Empty;
        public DateTimeOffset Prepared { get; set; }
    }
}
