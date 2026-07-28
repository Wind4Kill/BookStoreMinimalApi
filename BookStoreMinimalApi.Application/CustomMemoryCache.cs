using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace BookStoreMinimalApi.Api.Endpoints
{
    public class CustomMemoryCache
    {
        public MemoryCache Cache { get; } = new MemoryCache(new MemoryCacheOptions() { SizeLimit = 1024 });
    }
}