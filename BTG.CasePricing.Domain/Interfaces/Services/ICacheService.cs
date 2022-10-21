using Microsoft.Extensions.Caching.Distributed;

namespace BTG.CasePricing.Domain.Interfaces.Services;

public interface ICacheService
{
    byte[] Get(string key);
    Task<byte[]> GetAsync(string key, CancellationToken token = default);
    void Refresh(string key);
    Task RefreshAsync(string key, CancellationToken token = default);
    void Remove(string key);
    Task RemoveAsync(string key, CancellationToken token = default);
    void Set(string key, byte[] value, DistributedCacheEntryOptions options);
    Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default);
}