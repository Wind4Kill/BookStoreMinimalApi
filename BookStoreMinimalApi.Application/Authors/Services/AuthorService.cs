
using BookStoreMinimalApi.Application.Interfaces.Repositories;
using BookStoreMinimalApi.Application.Interfaces.Services;
using BookStoreMinimalApi.Domain.Entities;

namespace BookStoreMinimalApi.Application.Authors.Services
{
      public class AuthorService : IAuthorService
      {
            readonly IAuthorRepository _authorRepository;

            public AuthorService(IAuthorRepository authorRepository)
            {
                  _authorRepository = authorRepository;
            }
            public async Task<Author?> CheckExistingAuthor(string authorName, CancellationToken cancellationToken)
            {
                  Author? checkAuthor = await _authorRepository.GetAuthorByName(authorName, cancellationToken);

                  return checkAuthor;
            }
      }
}