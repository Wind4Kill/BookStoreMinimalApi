using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Domain.Entities;

namespace BookStoreMinimalApi.Domain.Interfaces.Repositories
{
    public interface IAuthorRepository
    {
        Task<Author?> GetAuthorByName(string authorName);

        Task<Author?> GetAuthorById(int id);
    }
}