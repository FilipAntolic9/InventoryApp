using System;
using System.Collections.Generic;
using System.Linq;
using InventoryApp.Application.DTOs;

namespace InventoryApp.Web.ViewModels
{
    public class ProductListVm
    {
        private const int PageSize = 10;
        public IEnumerable<ProductDto> Products { get; }
        public string Search { get; }
        public int Page { get; }
        public int TotalPages { get; }

        public ProductListVm(IEnumerable<ProductDto> all, string search, int page)
        {
            Search = search;
            var filtered = string.IsNullOrWhiteSpace(search)
                ? all
                : all.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            Page = page;
            TotalPages = (int)Math.Ceiling(filtered.Count() / (double)PageSize);
            Products = filtered
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }
}