using FtelMarket.Entities;
using Microsoft.EntityFrameworkCore;

namespace FtelMarket.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Category>(e =>
        {
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.HasData(
                new Category { Id = 1, Name = "PlayStation 5" },
                new Category { Id = 2, Name = "Nintendo Switch" },
                new Category { Id = 3, Name = "Xbox Series X" },
                new Category { Id = 4, Name = "PC" });
        });

        b.Entity<Product>(e =>
        {
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.Property(x => x.Description).HasMaxLength(1000);
            e.Property(x => x.Price).HasPrecision(18, 2);  
            e.HasOne(x => x.Category)
             .WithMany(c => c.Products)
             .HasForeignKey(x => x.CategoryId)
             .OnDelete(DeleteBehavior.Restrict);
        });
    }
}