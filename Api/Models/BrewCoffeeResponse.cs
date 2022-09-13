namespace Api.Models
{
    public class BrewCoffeeResponse
    {
        public string Message { get; set; } = string.Empty;
        public DateTimeOffset Prepared { get; set; }
    }
}
