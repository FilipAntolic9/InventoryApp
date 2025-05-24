using System;
using System.Text.RegularExpressions;

namespace InventoryApp.Domain.ValueObjects
{
    public class EmailAddress
    {
        private static readonly Regex _pattern = 
            new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        public string Value { get; }

        private EmailAddress(string value) => Value = value;

        public static EmailAddress Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.", nameof(email));

            var trimmed = email.Trim();
            if (!_pattern.IsMatch(trimmed))
                throw new ArgumentException("Invalid email format.", nameof(email));

            return new EmailAddress(trimmed);
        }

        public override string ToString() => Value;
    }
}
