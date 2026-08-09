using System;
using System.ComponentModel.DataAnnotations;
using BookStoreMinimalApi.Domain.FiltrationEntities;

namespace BookStoreMinimalApi.Endpoints
{
    public class Filters
    {
        public string? FilterOptions { get; set; }

        public string? OrderOptions { get; set; }

        public string? FilterValue { get; set; }

        public int? PageNum { get; set; }
    }
}