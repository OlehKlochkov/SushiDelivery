using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Moq;
using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.DAL.Repositories;
using SushiDelivery.DAL.Tests.Infrastructure;
using SushiDelivery.Domain.Enumerations;
using SushiDelivery.Domain.Models;
using System.ComponentModel;

using Xunit.Abstractions;

namespace SushiDelivery.DAL.Tests.Repositories
{
    public class ProductRepositoryTests : TestBase
    {

        public ProductRepositoryTests(ITestOutputHelper output)
            : base(output)
        {
        }


        [Fact]
        [Description("Test verifies get by id entity from database.")]
        public void TestGetByIdAsync()
        {
            //TODO:
        }

        [Fact]
        [Description("Test verifies include data into the database.")]
        public async Task TestUpdateAsync()
        {
            // Arrange
            var newProduct = new Product
            {
                Id = new Id<IProductId>(Guid.NewGuid()),
                Name = "TestName",
                Price = (decimal)0.99,
                IsAvailable = true,
                Category = Categories.Dessert
            };

            var existingProduct = new Models.Product
            {
                Id = newProduct.Id,
                Name = "OLDName",
                Price = (decimal)1.23,
                IsAvailable = false,
                Category = Categories.Pizza
            };

            var logMock = new Mock<ILogger>();

            var mockSet = new Mock<DbSet<IProduct>>(MockBehavior.Strict);
            mockSet.Setup(m => m.FindAsync(It.IsAny<Guid>())).ReturnsAsync(existingProduct);

            var contextMock = new Mock<ISushiDeliveryContext>(MockBehavior.Strict);
            contextMock.Setup(m => m.GetDbSet<IProduct>()).Returns(mockSet.Object);
            contextMock.Setup(m => m.SetModified<IProduct>(existingProduct));
            contextMock.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);

            var productRepository = new ProductRepository(
                new Lazy<ISushiDeliveryContext>(contextMock.Object),
                logMock.Object);

            // Act
            var result = await productRepository.UpdateAsync(newProduct);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Entity);
            Assert.NotNull(existingProduct);
            Assert.Equal(existingProduct, result.Entity);
            Assert.Equal(existingProduct.Id, result.Entity?.Id);
            Assert.Equal(existingProduct.Name, result.Entity?.Name);
            Assert.Equal(existingProduct.Price, result.Entity?.Price);
            Assert.Equal(existingProduct.IsAvailable, result.Entity?.IsAvailable);
            Assert.Equal(existingProduct.Category, result.Entity?.Category);
            Assert.False(result.WasOverriden);
            Assert.Equal(1, result.Count);
            mockSet.Verify(x => x.FindAsync(It.IsAny<Guid>()), Times.Once);
            contextMock.Verify(m => m.SetModified<IProduct>(existingProduct), Times.Once);
            contextMock.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }
    }



}
