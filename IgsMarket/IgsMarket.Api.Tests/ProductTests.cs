using FluentAssertions;
using IgsMarket.Api.Model;
using System;
using Xunit;

namespace IgsMarket.Api.Tests
{
    public class ProductTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void WhenNewCreateUserWithInvalidName_ThenThrowsArgumentException(string productName)
        {
            // Arrange
            var productPrice = 123;

            // Act
            Action action = () => new Product(productName, productPrice);

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-123)]
        public void WhenNewCreateUserWithInvalidPrice_ThenThrowsArgumentException(double productPrice)
        {
            // Arrange
            var productName = "product name";

            // Act
            Action action = () => new Product(productName, productPrice);

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void WhenNewCreateWithValidParams_ThenNewUserCreatedWithCorrectValues()
        {
            // Arrange
            var productName = "product name";
            var productPrice = 123;

            // Act
            var product = new Product(productName, productPrice);

            // Assert
            product.Name.Should().Be(productName);
            product.Price.Should().Be(productPrice);
        }

        [Fact]
        public void WhenSetName_ThenProductNameChanged()
        {
            // Arrange
            var product = new Product("product name", 123);

            // Act
            var newProductName = "product name";
            product.SetName(newProductName);

            // Assert
            product.Name.Should().Be(newProductName);
        }

        [Fact]
        public void WhenSetPrice_ThenProductPriceChanged()
        {
            // Arrange
            var product = new Product("product name", 123);

            // Act
            var newProductPrice = 123;
            product.SetPrice(newProductPrice);

            // Assert
            product.Price.Should().Be(newProductPrice);
        }
    }
}
