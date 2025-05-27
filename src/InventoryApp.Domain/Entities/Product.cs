using System;
using System.Collections.Generic;
using InventoryApp.Domain.ValueObjects;

namespace InventoryApp.Domain.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        private readonly List<InventoryLocation> _stockLocations = new();
        public IReadOnlyCollection<InventoryLocation> StockLocations
            => _stockLocations.AsReadOnly();

        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyCollection<OrderItem> OrderItems
            => _orderItems.AsReadOnly();

        private Product() { }

        public Product(int id, string name, string description, decimal price)
        {
            Id          = id;
            Name        = name        ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            if (price < 0) throw new ArgumentOutOfRangeException(nameof(price));
            Price       = price;
        }

        public void ChangePrice(decimal newPrice)
        {
            if (newPrice < 0) throw new ArgumentOutOfRangeException(nameof(newPrice));
            Price = newPrice;
        }
    }
}
