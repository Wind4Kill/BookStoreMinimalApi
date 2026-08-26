

using BookStoreMinimalApi.Application.Interfaces.Repositories;
using BookStoreMinimalApi.Application.Interfaces.Services;
using BookStoreMinimalApi.Domain.Entities;

namespace BookStoreMinimalApi.Application.Categories.Services
{
      public class CategoryService : ICategoryService
      {
            readonly ICategoryRepository _categoryRepository;

            public CategoryService(ICategoryRepository categoryRepository)
            {
                  _categoryRepository = categoryRepository;
            }
            public async Task<List<Category>> CheckExistingCategories(string[] categoriesNames, CancellationToken cancellationToken)
            {
                  List<Category> bookCategories = new List<Category>();
                  List<Category>? checkCategories = await _categoryRepository.GetCategoriesByName(categoriesNames, cancellationToken);

                  if (checkCategories is not null)
                  {
                        string[]? absentCategoryNames = categoriesNames.Except(checkCategories?.Select(c => c.CategoryName).ToArray()!).ToArray();
                        foreach (string category in absentCategoryNames)
                        {
                              bookCategories.Add(new Category { CategoryName = category });
                        }
                        bookCategories.AddRange(checkCategories!);
                  }
                  else
                  {
                        foreach (string category in categoriesNames)
                        {
                              bookCategories.Add(new Category { CategoryName = category });
                        }
                  }

                  return bookCategories;

            }
      }
}