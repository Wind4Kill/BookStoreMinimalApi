using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Domain.Entities;
using BookStoreMinimalApi.Domain.Interfaces.Repositories;
using BookStoreMinimalApi.Domain.Interfaces.Services;

namespace BookStoreMinimalApi.Application
{
      public class AuthorService : IAuthorService
      {
            readonly IAuthorRepository _authorRepository;

            public AuthorService(IAuthorRepository authorRepository)
            {
                  _authorRepository = authorRepository;
            }
            public async Task<Author?> CheckExistingAuthor(string authorName)
            {
                  Author? checkAuthor = await _authorRepository.GetAuthorByName(authorName);

                  return checkAuthor;
            }
      }
}