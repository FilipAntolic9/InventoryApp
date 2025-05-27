namespace InventoryApp.Domain.Entities
{
    public class Supplier
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        private Supplier() { }

        public Supplier(int id, string name)
        {
            Id = id;
            Name = !string.IsNullOrWhiteSpace(name)
                ? name
                : throw new ArgumentException("ERROR: Name is required.", nameof(name));
        }

        public void ChangeName(string newName)
        {
            Name = !string.IsNullOrWhiteSpace(newName)
                ? newName
                : throw new ArgumentException("ERROR: Name is required.", nameof(newName));
        }
    }
}