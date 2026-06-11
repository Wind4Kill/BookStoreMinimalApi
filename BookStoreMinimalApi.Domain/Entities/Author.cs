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

        public int Years => CalculateAge(_dateOfBirth, DateTime.Now);
        public void SetBirthDate(DateTime birthDate)
        {
            _dateOfBirth = birthDate;
        }
        public static int CalculateAge(DateTime start, DateTime end)
        {
            return (end.Year - start.Year - 1) +
            (((end.Month > start.Month) ||
            ((end.Month == start.Month)
            && (end.Day >= start.Day)))
            ? 1 : 0);
        }

    }
}