namespace AspNetCoreHero.Boilerplate.Application.DTOs.Settings
{
    public class CacheSettings
    {
        public int AbsoluteExpirationInMinutes { get; set; }
        public int SlidingExpirationInMinutes { get; set; }
    }
}