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
        public async Task<Category?> GetCategoryById(int id)
        {
            Category? requestedCategory = await _context.Categories.SingleOrDefaultAsync(c => c.CategoryId == id);
            return requestedCategory;
        }

        public async Task<List<Category>?> GetCategoriesByName(params string[] names)
        {
            List<Category>? requestedCategories = await _context.Categories.Where(c=>names.Contains(c.CategoryName)).ToListAsync();
            return requestedCategories;
        }
    }
}