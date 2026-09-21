using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMinimalApi.Application.Interfaces.Abstractions
{
    public interface ICacheService<T> where T:class
    {
        T? GetFromCache(int id, CancellationToken cancellationToken = default);

        void AddToCache(T entity, int id, CancellationToken cancellationToken = default);

        void RemoveFromCache(int id);
    }
}