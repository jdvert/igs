using IgsMarket.Api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IgsMarket.Api.Services
{
    internal class EFProductRepository : IProductRepository
    {
        private readonly ILogger<EFProductRepository> _logger;
        private readonly IgsMarketDbContext _dbContext;

        public EFProductRepository(ILogger<EFProductRepository> logger, IgsMarketDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProduct(Product product)
        {
            _dbContext.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProduct(int productId)
        {
            return await _dbContext.Products.FindAsync(productId);
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _dbContext.Update(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }
    }
}
