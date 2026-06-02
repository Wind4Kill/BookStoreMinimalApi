using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BookStoreMinimalApi.Data;

namespace BookStoreMinimalApi.Domain.FiltrationEntities
{
    public static class FilterHelp
    {
        public static IQueryable<Book> FilterEntities(this IQueryable<Book> books, FilterOptions options, string filterValue)
        {
            return options switch
            {
                FilterOptions.None => books,
                FilterOptions.ByCost => books.Where(b => b.Cost <= decimal.Parse(filterValue)),
                _ => books
            };
        }

        public static IQueryable<Book> OrderEntities(this IQueryable<Book> books, OrderOptions options, string orderValue)
        {
            return options switch
            {
                OrderOptions.ByDefault => books.OrderBy(b => b.BookId),
                OrderOptions.ByCost => books.OrderBy(b => b.Cost),
                OrderOptions.ByTitle => books.OrderBy(b => b.Title),
                _ => books.OrderBy(b => b.BookId)
            };
        }

        public static IQueryable<Book> Paginate(this IQueryable<Book> books, int pageNum)
        {
            if (pageNum < 1)
            {
                throw new ArgumentException("Page number can't be less than 1");
            }

            return books.Skip((pageNum - 1) * 10).Take(10);
        }
    }
}