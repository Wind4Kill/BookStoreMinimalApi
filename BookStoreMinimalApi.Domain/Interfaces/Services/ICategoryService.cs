using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Domain.DTOs.BookDTOs;
using BookStoreMinimalApi.Domain.Entities;

namespace BookStoreMinimalApi.Domain.Interfaces.Services
{
    public interface ICategoryService
      {
            Task<List<Category>> CheckExistingCategories(CreateBookDto bookDto);
      }

}