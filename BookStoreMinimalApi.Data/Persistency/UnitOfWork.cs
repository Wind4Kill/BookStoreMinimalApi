using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Application.Interfaces.Abstractions;

namespace BookStoreMinimalApi.Data.Persistency
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationContext _dbContext;
        public UnitOfWork(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}