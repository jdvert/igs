using FluentAssertions;
using IgsMarket.Api.Model;
using IgsMarket.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IgsMarket.Api.Tests
{
    public class EFRepositoryTests
    {
        private IgsMarketDbContext _dbContext;
        private IProductRepository _productRepository;

        public EFRepositoryTests()
        {
            CreateDatabase();

            _productRepository = new EFProductRepository(
                new Mock<ILogger<EFProductRepository>>().Object,
                _dbContext
            );
        }

        [Fact]
        public async Task WhenCreateUser_ThenUserSavedToDatabase()
        {
            // Arrange
            var productName = "test product";
            var product = new Product(productName, 123);

            // Act
            await _productRepository.CreateProduct(product);

            // Assert
            (await _dbContext.Products.CountAsync()).Should().Be(1);
        }

        [Fact]
        public async Task WhenDeleteUser_ThenUserRemovedFromDatabase()
        {
            // Arrange
            var productId = 1;
            var existingProduct = new Product(productId, "test product", 123);

            await _dbContext.AddAsync(existingProduct);
            await _dbContext.SaveChangesAsync();

            // Act
            await _productRepository.DeleteProduct(existingProduct);

            // Assert
            (await _dbContext.Products.CountAsync()).Should().Be(0);
        }

        [Fact]
        public async Task WhenGetAllProducts_AndNoProductsExist_ThenReturnsEmpty()
        {
            // Act
            var result = await _productRepository.GetAllProducts();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task WhenGetAllProducts_ThenAllProductsReturned()
        {
            // Arrange
            var existingProducts = new List<Product>()
            {
                new Product(1, "test product", 123),
                new Product(2, "another test product", 321)
            };

            await _dbContext.AddRangeAsync(existingProducts);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _productRepository.GetAllProducts();

            // Assert
            result.Count().Should().Be(existingProducts.Count());
        }

        [Fact]
        public async Task WhenGetProduct_AndProductDoesntExist_ThenReturnsNull()
        {
            // Arrange
            var productId = 1;

            // Act
            var result = await _productRepository.GetProduct(productId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task WhenGetProduct_ThenReturnsNotNull()
        {
            // Arrange
            var productId = 1;
            var existingProduct = new Product(productId, "test product", 123);

            await _dbContext.AddAsync(existingProduct);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _productRepository.GetProduct(productId);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task WhenUpdateProduct_ThenProductUpdatedInDatabase()
        {
            // Arrange
            var productId = 1;
            var existingProduct = new Product(productId, "test product", 123);

            await _dbContext.AddAsync(existingProduct);
            await _dbContext.SaveChangesAsync();

            // Act
            var newName = "new product name";
            var newPrice = 321;

            existingProduct.SetName(newName);
            existingProduct.SetPrice(newPrice);

            var updatedProduct = await _productRepository.UpdateProduct(existingProduct);

            // Assert
            updatedProduct.Name.Should().Be(newName);
            updatedProduct.Price.Should().Be(newPrice);
        }

        private void CreateDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<IgsMarketDbContext>();
            dbOptions.UseInMemoryDatabase(databaseName: "TestDb");
            _dbContext = new IgsMarketDbContext(dbOptions.Options, seedData: false);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }
    }
}
