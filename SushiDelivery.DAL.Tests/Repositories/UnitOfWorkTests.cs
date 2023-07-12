using Moq;
using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.DAL.Repositories;
using System.ComponentModel;

namespace SushiDelivery.DAL.Tests.Repositories
{
    public class UnitOfWorkTests
    {
        private Mock<ISushiDeliveryContext> _contextMock;
        private UnitOfWork _unitOfWork;

        public UnitOfWorkTests()
        {
            _contextMock = new Mock<ISushiDeliveryContext>();
            _unitOfWork = new UnitOfWork(_contextMock.Object);
        }

        [Fact]
        [Description("Test verifies SaveChanges() method.")]
        public async Task TestSaveChanges()
        {
            //Act
            await _unitOfWork.SaveChanges();

            //Assert
            _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        [Description("Test verifies that one instance of the repositories are created.")]
        public void TestGetRepositories()
        {
            //Act
            var customerRepository = _unitOfWork.CustomerRepository;
            var productRepository = _unitOfWork.ProductRepository;

            //Assert
            Assert.NotNull(customerRepository);
            Assert.Equal(customerRepository, _unitOfWork.CustomerRepository);

            Assert.NotNull(productRepository);
            Assert.Equal(productRepository, _unitOfWork.ProductRepository);
        }
    }
}
