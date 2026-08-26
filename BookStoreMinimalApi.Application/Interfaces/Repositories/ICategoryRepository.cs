using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Domain.Entities;

namespace BookStoreMinimalApi.Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetCategoryById(int id, CancellationToken cancellationToken);
        Task<List<Category>?> GetCategoriesByName(string[] names, CancellationToken cancellationToken);
    }
}