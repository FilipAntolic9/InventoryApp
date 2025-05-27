using System;
using InventoryApp.Domain.ValueObjects;

namespace InventoryApp.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public EmailAddress Email { get; private set; }
        public string PasswordHash { get; private set; }

        private User() { }

        public User(int id, string firstName, string lastName, EmailAddress email, string passwordHash)
        {
            Id           = id;
            FirstName    = firstName    ?? throw new ArgumentNullException(nameof(firstName));
            LastName     = lastName     ?? throw new ArgumentNullException(nameof(lastName));
            Email        = email        ?? throw new ArgumentNullException(nameof(email));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        }
    }
}
