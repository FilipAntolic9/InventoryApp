using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryApp.Domain.Entities
{
    public enum OrderStatus
    {
        Pending,
        Completed,
        Cancelled
    }

    public class Order
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public OrderStatus Status { get; private set; }

        private readonly List<OrderItem> _items = new();
        public IReadOnlyCollection<OrderItem> Items
            => _items.AsReadOnly();

        private Order() { }

        public Order(Guid id, Guid userId)
        {
            Id        = id;
            UserId    = userId;
            OrderDate = DateTime.UtcNow;
            Status    = OrderStatus.Pending;
        }

        public void AddItem(Product product, int quantity, decimal unitPrice)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0)   throw new ArgumentOutOfRangeException(nameof(quantity));
            if (unitPrice < 0)   throw new ArgumentOutOfRangeException(nameof(unitPrice));

            var existing = _items.FirstOrDefault(i => i.ProductId == product.Id);
            if (existing != null)
            {
                existing.IncreaseQuantity(quantity);
            }
            else
            {
                _items.Add(new OrderItem(Id, product.Id, quantity, unitPrice));
            }
        }

        public void RemoveItem(Guid productId)
        {
            var item = _items.FirstOrDefault(i => i.ProductId == productId);
            if (item == null) throw new InvalidOperationException("ERROR: No item to remove.");
            _items.Remove(item);
        }

        public void ChangeStatus(OrderStatus newStatus)
        {
            Status = newStatus;
        }
    }
}
