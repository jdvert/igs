using IgsMarket.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace IgsMarket.Api
{
    internal class IgsMarketDbContext : DbContext
    {
        private readonly bool _seedData;

        public DbSet<Product> Products { get; set; }

        public IgsMarketDbContext(DbContextOptions<IgsMarketDbContext> options, bool seedData = true)
        : base(options)
        {
            _seedData = seedData;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (_seedData)
            {
                modelBuilder.Entity<Product>()
                    .HasData(SeedData.Products);
            }

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Ignore(p => p.FormattedPrice);
        }
    }
}
