using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Data;

namespace BookStoreMinimalApi.Domain.Entities
{
    public class Author
    {
        public required string Name { get; set; }

        public int PublishedBooks { get; set; }

        public required ICollection<Book> Books { get; set; }
    }
}