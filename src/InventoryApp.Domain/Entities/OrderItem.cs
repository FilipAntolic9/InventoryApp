using System;

namespace InventoryApp.Domain.Entities
{
    public class OrderItem
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        public Order Order { get; private set; }
        public Product Product { get; private set; }

        private OrderItem() { }

        public OrderItem(Guid orderId, Guid productId, int quantity, decimal unitPrice)
        {
            OrderId = orderId;
            ProductId = productId;
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
            if (unitPrice < 0) throw new ArgumentOutOfRangeException(nameof(unitPrice));
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        internal void IncreaseQuantity(int delta)
        {
            if (delta <= 0) throw new ArgumentOutOfRangeException(nameof(delta));
            Quantity += delta;
        }
    }
}
