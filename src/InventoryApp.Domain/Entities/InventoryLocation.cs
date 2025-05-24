using System;

namespace InventoryApp.Domain.Entities
{
    public class InventoryLocation
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public string Location { get; private set; }

        public Product Product { get; private set; }

        private InventoryLocation() { }

        public InventoryLocation(Guid id, Guid productId, int initialQuantity, string location)
        {
            Id         = id;
            ProductId  = productId;
            Quantity   = initialQuantity >= 0 
                         ? initialQuantity 
                         : throw new ArgumentOutOfRangeException(nameof(initialQuantity));
            Location   = location ?? throw new ArgumentNullException(nameof(location));
        }

        public void AdjustQuantity(int delta)
        {
            var newQty = Quantity + delta;
            if (newQty < 0) 
                throw new InvalidOperationException("ERROR: Quantity < 0");
            Quantity = newQty;
        }
    }
}
