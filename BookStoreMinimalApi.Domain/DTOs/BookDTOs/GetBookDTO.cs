using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreMinimalApi.Data;

namespace BookStoreMinimalApi.Domain.DTOs.BookDTOs
{
    [AutoMap(typeof(Book))]
    public class GetBookDTO
    {
        public int BookId { get; set; }
        public required string Title { get; set; }
        public decimal Cost { get; set; }
    }
}