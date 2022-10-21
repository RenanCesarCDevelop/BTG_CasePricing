using BTG.CasePricing.Application.Interfaces;

namespace BTG.CasePricing.Application.Settings;

public class CacheSettings : IAppSettings
{
    public bool Enabled { get; set; }
    public bool PreferRedis { get; set; }
    public string RedisConnectionString { get; set; }
    public int AbsoluteExpirationInSeconds { get; set; }
    public int SlidingExpirationInSeconds { get; set; }
}