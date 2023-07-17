using System.ComponentModel;

using Microsoft.Extensions.Logging;

using Moq;

using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.DAL.Repositories;
using SushiDelivery.DAL.Tests.Infrastructure;

using Xunit.Abstractions;

namespace SushiDelivery.DAL.Tests.Repositories
{
    public class UnitOfWorkTests : TestBase
    {
        public UnitOfWorkTests(ITestOutputHelper output)
            : base(output)
        {
        }

        [Fact]
        [Description("Test verifies SaveChanges() method.")]
        public async Task SaveChangesAsync_NoChanges()
        {
            // Arrange
            var contextMock = new Mock<ISushiDeliveryContext>();
            var loggerMock = new Mock<ILogger>();
            var unitOfWork = new UnitOfWork(new Lazy<ISushiDeliveryContext>(contextMock.Object), loggerMock.Object);

            //Act
            var result = await unitOfWork.SaveChangesAsync();

            //Assert
            Assert.Null(result.Entity);
            Assert.False(result.WasOverriden);
            Assert.Equal(0, result.Count);
            contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
            
        }

        [Fact]
        [Description("Test verifies that one instance of the repositories are created.")]
        public void TestGetRepositories()
        {
            // Arrange
            var contextMock = new Mock<ISushiDeliveryContext>();
            var loggerMock = new Mock<ILogger>();
            var unitOfWork = new UnitOfWork(new Lazy<ISushiDeliveryContext>(contextMock.Object), loggerMock.Object);

            //Act
            var customerRepository = unitOfWork.CustomerRepository;
            var productRepository = unitOfWork.ProductRepository;

            //Assert
            Assert.NotNull(customerRepository);
            Assert.False(customerRepository.AutoSaveChanges);
            Assert.Equal(customerRepository, unitOfWork.CustomerRepository);

            Assert.NotNull(productRepository);
            Assert.False(productRepository.AutoSaveChanges);
            Assert.Equal(productRepository, unitOfWork.ProductRepository);
        }
    }
}
