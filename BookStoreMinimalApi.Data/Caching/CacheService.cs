using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Api.Endpoints;
using BookStoreMinimalApi.Application.Interfaces.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace BookStoreMinimalApi.Data.Caching
{
    public class CacheService<T>(CustomMemoryCache cache) : ICacheService<T> where T : class
    {
        public void AddToCache(T entity, int id, CancellationToken cancellationToken = default)
        {
            string key = CreateCacheKey(typeof(T), id);
            MemoryCacheEntryOptions cacheOptions = new()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(5),
                Size = 1
            };
            cache.Cache.Set<T>(key, entity, cacheOptions);
        }

        public T? GetFromCache(int id, CancellationToken cancellationToken = default)
        {
            string key = CreateCacheKey(typeof(T), id);

            cache.Cache.TryGetValue<T>(key, out T? result);
            return result;
        }

        public void RemoveFromCache(int id)
        {
            string key = CreateCacheKey(typeof(T), id);
            cache.Cache.Remove(key);
        }

        private string CreateCacheKey(Type type, int id) => $"{nameof(type):id}:";
    }
}