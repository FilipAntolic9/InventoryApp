using System;

namespace InventoryApp.Application.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } // ProductCode.Value
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
