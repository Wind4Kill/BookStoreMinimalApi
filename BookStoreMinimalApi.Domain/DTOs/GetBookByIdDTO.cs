using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreMinimalApi.Data;

namespace BookStoreMinimalApi.Domain.DTOs
{
    [AutoMap(typeof(Book))]
    public class GetBookByIdDTO
    {
        public required string Title { get; set; }

        public decimal Cost { get; set; }

        public required string Description { get; set; }

        public required string AuthorName { get; set; }

    }
}