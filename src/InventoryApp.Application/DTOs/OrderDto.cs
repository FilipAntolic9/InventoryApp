using System;
using System.Collections.Generic;

namespace InventoryApp.Application.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }

        public List<OrderItemDto> Items { get; set; }
            = new List<OrderItemDto>();
    }
}
