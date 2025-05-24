using System;

namespace InventoryApp.Domain.ValueObjects
{
    public class ProductCode
    {
        public string Value { get; }

        private ProductCode(string code) => Value = code;

        public static ProductCode Create(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Product code is required.", nameof(code));

            var trimmed = code.Trim();
            if (trimmed.Length > 20)
                throw new ArgumentException("Maximum code length is 20 characters.", nameof(code));

            return new ProductCode(trimmed);
        }

        public override string ToString() => Value;
    }
}
