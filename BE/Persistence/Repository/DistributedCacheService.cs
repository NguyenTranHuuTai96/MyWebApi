
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IRepositories;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Persistence.Repository
{
    public class DistributedCacheService : IDistributedCacheService
    {
        private readonly IDistributedCache _cache;

        public DistributedCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task Set<T>(string key, T value)
        {
            await _cache.SetStringAsync(key, JsonSerializer.Serialize(value), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(3),
                SlidingExpiration = TimeSpan.FromMinutes(20)
            });
        }

        public async Task<T> Get<T>(string key)
        {
            string data = await _cache.GetStringAsync(key)??string.Empty;
            T rs;
            if (!string.IsNullOrEmpty(data))
            {
                return JsonSerializer.Deserialize<T>(data);
            }

            return default;
        }

        public async Task Remove(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
