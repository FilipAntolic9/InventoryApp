using InventoryApp.Domain.Entities;
using InventoryApp.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InventoryLocation> InventoryLocations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("korisnik");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Id)
                    .HasColumnName("id");

                entity.Property(u => u.FirstName)
                    .HasColumnName("ime");

                entity.Property(u => u.LastName)
                    .HasColumnName("prezime");

                entity.Property(u => u.PasswordHash)
                    .HasColumnName("lozinka");

                entity.Property(u => u.Email)
                    .HasColumnName("email")
                    .HasConversion(
                        v => v.Value,
                        v => EmailAddress.Create(v) 
                    );
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("proizvod");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id)
                    .HasColumnName("id");

                entity.Property(p => p.Name)
                    .HasColumnName("naziv");

                entity.Property(p => p.Description)
                    .HasColumnName("opis");

                entity.Property(p => p.Price)
                    .HasColumnName("cijena");

            });
            modelBuilder.Entity<InventoryLocation>(entity =>
            {
                entity.ToTable("zaliha");

                entity.HasKey(il => il.Id);

                entity.Property(il => il.Id)
                      .HasColumnName("id");

                entity.Property(il => il.ProductId)
                      .HasColumnName("proizvod_id");

                entity.Property(il => il.Quantity)
                      .HasColumnName("kolicina");

                entity.Property(il => il.Location)
                      .HasColumnName("lokacija");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("narudzba");

                entity.HasKey(o => o.Id);

                entity.Property(o => o.Id)
                      .HasColumnName("id");

                entity.Property(o => o.VoditeljId)
                      .HasColumnName("voditelj_id");

                entity.Property(o => o.DobavljacId)
                      .HasColumnName("dobavljac_id");

                entity.Property(o => o.Datum)
                      .HasColumnName("datum");

                entity.Property(o => o.Status)
                      .HasColumnName("status");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("dobavljac");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("korisnik_id");

                entity.Property(e => e.Name)
                      .HasColumnName("naziv")
                      .HasMaxLength(100);
            });

            
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("narudzba_stavka");

                entity.HasKey(e => new { e.OrderId, e.ProductId });

                entity.Property(e => e.OrderId).HasColumnName("narudzba_id");
                entity.Property(e => e.ProductId).HasColumnName("proizvod_id");
                entity.Property(e => e.Quantity).HasColumnName("kolicina");
                entity.Property(e => e.UnitPrice).HasColumnName("cijena");

                entity.HasOne(e => e.Order)
                      .WithMany(o => o.Items)
                      .HasForeignKey(e => e.OrderId);

                entity.HasOne(e => e.Product)
                      .WithMany()
                      .HasForeignKey(e => e.ProductId);
            });

        }
    }
}
