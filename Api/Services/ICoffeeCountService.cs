namespace Api.Services
{
    public interface ICoffeeCountService
    {
        public int Increase();
        public void Reset();
        public int Value { get; }
    }
}
