using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Domain.Entities;

namespace BookStoreMinimalApi.Domain.Interfaces.Services
{
     public interface IAuthorService
      {
            Task<Author?> CheckExistingAuthor(string authorName);
      }
}