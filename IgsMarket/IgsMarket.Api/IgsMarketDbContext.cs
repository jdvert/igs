using IgsMarket.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace IgsMarket.Api
{
    internal class IgsMarketDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public IgsMarketDbContext(DbContextOptions<IgsMarketDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasData(SeedData.Products);

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Ignore(p => p.FormattedPrice);
        }
    }
}
