namespace Api.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static bool IsAprilFools(this DateTimeOffset timeOffset)
        {
            return timeOffset.Month == 4 && timeOffset.Day == 1;
        }
    }
}
