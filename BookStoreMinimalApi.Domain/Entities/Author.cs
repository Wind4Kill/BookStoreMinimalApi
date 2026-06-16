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
       public int Years => CalculateAge(_dateOfBirth);


        public Author() { }

        public Author(DateTime birthDay) : base()
        {
            _dateOfBirth = birthDay;
        }

        public static int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            int age = today.Year - birthDate.Year;

            if (birthDate.AddYears(age) > today)
                age--;

            return age;
        }

    }
}