using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Domain.Entities;
using BookStoreMinimalApi.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookStoreMinimalApi.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        readonly ApplicationContext _context;
        public CategoryRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Category?> GetCategoryById(int id, CancellationToken cancellationToken)
        {
            Category? requestedCategory = await _context.Categories.
            SingleOrDefaultAsync(c => c.CategoryId == id, cancellationToken);
            return requestedCategory;
        }

        public async Task<List<Category>?> GetCategoriesByName(string[] names, CancellationToken cancellationToken)
        {
            List<Category>? requestedCategories = await _context.Categories.
            Where(c=>names.Contains(c.CategoryName)).ToListAsync(cancellationToken);
            return requestedCategories;
        }
    }
}