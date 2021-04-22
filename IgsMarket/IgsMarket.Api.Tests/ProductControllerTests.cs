using FluentAssertions;
using IgsMarket.Api.Controllers;
using IgsMarket.Api.Http;
using IgsMarket.Api.Model;
using IgsMarket.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IgsMarket.Api.Tests
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;

        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _controller = new ProductController(_mockProductRepository.Object);
        }

        [Fact]
        public async Task WhenGet_AndRepoReturnsProducts_ThenProductsReturned()
        {
            // Arrange
            var existingProducts = new List<Product>()
            {
                new Product(1, "test product", 123)
            };

            _mockProductRepository
                .Setup(x => x.GetAllProducts())
                .ReturnsAsync(existingProducts);

            // Act
            var result = await _controller.Get();

            // Assert
            result.Count().Should().Be(existingProducts.Count());
        }

        [Fact]
        public async Task WhenGet_AndRepoDoesntReturnProducts_ThenNoProductsReturned()
        {
            // Arrange
            _mockProductRepository
                .Setup(x => x.GetAllProducts())
                .ReturnsAsync(new List<Product>());

            // Act
            var result = await _controller.Get();

            // Assert
            result.Count().Should().Be(0);
        }

        [Fact]
        public async Task WhenGetById_AndProductExists_ThenProductReturned_AndStatusIs200()
        {
            // Arrange
            var existingProduct = new Product(1, "test product", 123);

            _mockProductRepository
                .Setup(x => x.GetProduct(It.IsAny<int>()))
                .ReturnsAsync(existingProduct);

            // Act
            var result = await _controller.Get(existingProduct.Id);

            // Assert
            result.Result.Should().BeOfType(typeof(OkObjectResult));
            (result.Result as OkObjectResult).Value.Should().NotBeNull();
        }

        [Fact]
        public async Task WhenGetById_AndProductDoesntExist_ThenStatusIs404()
        {
            // Arrange
            var productId = 123;

            _mockProductRepository
                .Setup(x => x.GetProduct(It.IsAny<int>()))
                .ReturnsAsync((Product)null);

            // Act
            var result = await _controller.Get(productId);

            // Assert
            result.Result.Should().BeOfType(typeof(NotFoundResult));
        }

        [Fact]
        public async Task WhenPost_ThenProductCreatedInRepo_AndStatusIs200()
        {
            // Arrange
            var request = new CreateProductRequest
            {
                Name = "test product",
                Price = 123
            };

            // Act
            var result = await _controller.Post(request);

            // Assert
            _mockProductRepository
                .Verify(x => x.CreateProduct(It.IsAny<Product>()), Times.Once);

            result.Should().BeOfType(typeof(OkResult));
        }

        [Fact]
        public async Task WhenPut_AndProductExists_AndNameChanged_ThenNameChangedInRepo_AndStatusIs200()
        {
            // Arrange
            var productId = 1;
            var existingProduct = new Product(productId, "test product", 123);

            _mockProductRepository
                .Setup(x => x.GetProduct(It.IsAny<int>()))
                .ReturnsAsync(existingProduct);

            var newName = "new product name";
            var request = new UpdateProductRequest
            {
                Name = newName
            };

            // Act
            var result = await _controller.Put(productId, request);

            // Assert
            _mockProductRepository
                .Verify(x => x.UpdateProduct(It.Is<Product>(y => y.Name == newName)), Times.Once);

            result.Should().BeOfType(typeof(OkResult));
        }

        [Fact]
        public async Task WhenPut_AndProductExists_AndPriceChanged_ThenNameChangedInRepo_AndStatusIs200()
        {
            // Arrange
            var productId = 1;
            var existingProduct = new Product(productId, "test product", 123);

            _mockProductRepository
                .Setup(x => x.GetProduct(It.IsAny<int>()))
                .ReturnsAsync(existingProduct);

            var newPrice = 321;
            var request = new UpdateProductRequest
            {
                Price = newPrice
            };

            // Act
            var result = await _controller.Put(productId, request);

            // Assert
            _mockProductRepository
                .Verify(x => x.UpdateProduct(It.Is<Product>(y => y.Price == newPrice)), Times.Once);

            result.Should().BeOfType(typeof(OkResult));
        }

        [Fact]
        public async Task WhenPut_AndProductDoesntExist_ThenStatusIs404()
        {
            // Arrange
            var productId = 1;

            _mockProductRepository
                .Setup(x => x.GetProduct(It.IsAny<int>()))
                .ReturnsAsync((Product)null);

            var request = new UpdateProductRequest();

            // Act
            var result = await _controller.Put(productId, request);

            // Assert
            _mockProductRepository
                .Verify(x => x.UpdateProduct(It.IsAny<Product>()), Times.Never);

            result.Should().BeOfType(typeof(NotFoundResult));
        }

        [Fact]
        public async Task WhenDelete_AndProductExists_ThenProductDeletedInRepo_AndStatusIs200()
        {
            // Arrange
            var productId = 1;
            var existingProduct = new Product(productId, "test product", 123);

            _mockProductRepository
                .Setup(x => x.GetProduct(It.IsAny<int>()))
                .ReturnsAsync(existingProduct);

            // Act
            var result = await _controller.Delete(productId);

            // Assert
            _mockProductRepository
                .Verify(x => x.DeleteProduct(It.IsAny<Product>()), Times.Once);

            result.Should().BeOfType(typeof(OkResult));
        }

        [Fact]
        public async Task WhenDelete_AndProductDoesntExist_ThenStatusIs404()
        {
            // Arrange
            var productId = 1;

            _mockProductRepository
                .Setup(x => x.GetProduct(It.IsAny<int>()))
                .ReturnsAsync((Product)null);

            // Act
            var result = await _controller.Delete(productId);

            // Assert
            _mockProductRepository
                .Verify(x => x.DeleteProduct(It.IsAny<Product>()), Times.Never);

            result.Should().BeOfType(typeof(NotFoundResult));
        }
    }
}
