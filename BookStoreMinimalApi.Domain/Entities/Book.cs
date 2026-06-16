using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreMinimalApi.Domain.Entities;

namespace BookStoreMinimalApi.Data
{
    public class Book
    {
        public int BookId { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public decimal Cost { get; set; }

        public bool IsDeleted { get; set; }

        public Author Author { get; set; } = null!;

        public required ICollection<Category> Categories { get; set; }

        public ICollection<Review> Reviews { get; set; } = null!;

        public int AuthorId { get; set; }

    }
}