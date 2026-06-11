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

        public int PublishedBooksCount { get; set; }

        public ICollection<Book>? Books { get; set; }
        private DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }
    }
}