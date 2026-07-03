using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Domain.DTOs.BookDTOs;
using BookStoreMinimalApi.Domain.Entities;
using BookStoreMinimalApi.Domain.Interfaces.Repositories;
using BookStoreMinimalApi.Domain.Interfaces.Services;

namespace BookStoreMinimalApi.Application
{
      public class CategoryService : ICategoryService
      {
            readonly ICategoryRepository _categoryRepository;

            public CategoryService(ICategoryRepository categoryRepository)
            {
                  _categoryRepository = categoryRepository;
            }
            public async Task<List<Category>> CheckExistingCategories(string[] categoriesNames)
            {
                  List<Category> bookCategories = new List<Category>();
                  List<Category>? checkCategories = await _categoryRepository.GetCategoriesByName(categoriesNames);

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