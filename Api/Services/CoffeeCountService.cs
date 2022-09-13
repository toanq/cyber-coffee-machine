namespace Api.Services
{
    public class CoffeeCountService : ICoffeeCountService
    {
        private int _count = 0;
        public CoffeeCountService()
        {

        }

        public int Increase() => _count++;

        public void Reset() => _count = 0;

        public int Value { get => _count; }
    }
}
