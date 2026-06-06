using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Data;

namespace BookStoreMinimalApi.Domain.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public required string Name { get; set; }

        public int PublishedBooks { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}