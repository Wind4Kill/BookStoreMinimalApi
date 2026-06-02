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

        public Filtration(FilterOptions filterOptions = FilterOptions.None,
        OrderOptions orderOptions = OrderOptions.ByDefault,
        string? filterValue = null, int pageNum = 1)
        {
            FilterOptions = filterOptions;
            OrderOptions = orderOptions;
            FilterValue = filterValue;
            PageNum = pageNum;
        }
    }
}