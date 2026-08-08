using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreMinimalApi.Domain.FiltrationEntities
{
    public class Filtration
    {
        public FilterOptions FilterOptions { get; set; }

        public OrderOptions OrderOptions { get; set; }

        public string? FilterValue { get; set; }

        public int PageNum { get; set; }

        public Filtration(string? filterOptions,
        string? orderOptions,
        string? filterValue, int? pageNum)
        {
            if (Enum.TryParse<FilterOptions>(filterOptions, out FilterOptions filterType))
            {
                FilterOptions = filterType;
            }
            if (Enum.TryParse<OrderOptions>(orderOptions, out OrderOptions orderType))
            {
                OrderOptions = orderType;
            }
            if (!string.IsNullOrEmpty(filterValue) && filterValue != " ")
            {
                FilterValue = filterValue;
            }

            PageNum = pageNum.HasValue ? pageNum.Value : 1;
        }
    }
}