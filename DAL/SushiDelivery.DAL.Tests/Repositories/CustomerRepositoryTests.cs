using System.ComponentModel;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Moq;

using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.DAL.Repositories;
using SushiDelivery.DAL.Tests.Infrastructure;
using SushiDelivery.Domain.Models;

using Xunit.Abstractions;

namespace SushiDelivery.DAL.Tests.Repositories
{
    public class CustomerRepositoryTests : TestBase
    {
        public CustomerRepositoryTests(ITestOutputHelper output)
            : base(output)
        {
        }



        [Fact]
        [Description("Test verifies get by id entity from database.")]
        public async Task TestGetByIdAsync()
        {
            // Arrange
            var expectedCustomer = new Models.Customer
            {
                Id = new Id<ICustomerId>(Guid.NewGuid()),
                LoginName = "TestLoginName",
                Address = "TestAdress",
                Phone = "TestPhoneNumber"
            };

            var logMock = new Mock<ILogger>();

            var mockSet = new Mock<DbSet<ICustomer>>(MockBehavior.Strict);
            mockSet.Setup(m => m.FindAsync(It.IsAny<Id<ICustomerId>>())).ReturnsAsync(expectedCustomer);

            var contextMock = new Mock<ISushiDeliveryContext>(MockBehavior.Strict);
            contextMock.Setup(m => m.GetDbSet<ICustomer>()).Returns(mockSet.Object);
            contextMock.Setup(m => m.SetDetached<ICustomer>(expectedCustomer));

            var customerRepository = new CustomerRepository(
                new Lazy<ISushiDeliveryContext>(contextMock.Object),
                logMock.Object);

            // Act
            var result = await customerRepository.GetByIdAsync(expectedCustomer.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCustomer, result);
            mockSet.Verify(x => x.FindAsync(It.IsAny<Id<ICustomerId>>()), Times.Once);
            contextMock.Verify(m => m.SetDetached(result), Times.Once);
        }

        [Fact]
        [Description("Test verifies save data into the database.")]
        public async Task TestCreateAsync_New()
        {
            // Arrange
            var newCustomer = new Customer
            {
                LoginName = "TestLoginName",
                Address = "TestAdress",
                Phone = "TestPhoneNumber"
            };

            Models.Customer? createdCustomer = null;

            var logMock = new Mock<ILogger>();

            var mockSet = new Mock<DbSet<ICustomer>>(MockBehavior.Strict);
            mockSet.Setup(m => m.AddAsync(It.IsAny<ICustomer>(), default))
                .Callback(
                    (ICustomer entity, CancellationToken token) =>
                    {
                        createdCustomer = entity as Models.Customer; 
                        createdCustomer.Id = new Id<ICustomerId>(Guid.NewGuid());
                    })
                .Returns(null);

            var contextMock = new Mock<ISushiDeliveryContext>(MockBehavior.Strict);
            contextMock.Setup(m => m.GetDbSet<ICustomer>()).Returns(mockSet.Object);
            contextMock.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);

            var customerRepository = new CustomerRepository(
                new Lazy<ISushiDeliveryContext>(contextMock.Object),
                logMock.Object);

            // Act
            var result = await customerRepository.CreateAsync(newCustomer);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Entity);
            Assert.NotNull(createdCustomer);
            Assert.Equal(createdCustomer, result.Entity);
            Assert.Equal(createdCustomer.Id, result.Entity?.Id);
            Assert.False(result.WasOverriden);
            Assert.Equal(1, result.Count);
            mockSet.Verify(x => x.FindAsync(It.IsAny<ICustomerId>()), Times.Never);
            mockSet.Verify(m => m.AddAsync(It.IsAny<Models.Customer>(),default), Times.Once);
            contextMock.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        [Description("Test verifies save data into the database.")]
        public async Task TestCreateAsync_Upsert()
        {
            // Arrange
            var newCustomer = new Customer
            {
                Id = new Id<ICustomerId>(Guid.NewGuid()),
                LoginName = "TestLoginName",
                Address = "TestAdress",
                Phone = "TestPhoneNumber"
            };

            var exisitngCustomer = new Models.Customer
            {
                Id = newCustomer.Id,
                LoginName = "OLDLoginName",
                Address = "OLDAdress",
                Phone = "OLDPhoneNumber"
            };

            var logMock = new Mock<ILogger>();

            var mockSet = new Mock<DbSet<ICustomer>>(MockBehavior.Strict);
            mockSet.Setup(m => m.FindAsync(It.IsAny<Guid>())).ReturnsAsync(exisitngCustomer);

            var contextMock = new Mock<ISushiDeliveryContext>(MockBehavior.Strict);
            contextMock.Setup(m => m.GetDbSet<ICustomer>()).Returns(mockSet.Object);
            contextMock.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);

            var customerRepository = new CustomerRepository(
                new Lazy<ISushiDeliveryContext>(contextMock.Object),
                logMock.Object);

            // Act
            var result = await customerRepository.CreateAsync(newCustomer);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Entity);
            Assert.NotNull(exisitngCustomer);
            Assert.Equal(exisitngCustomer, result.Entity);
            Assert.Equal(exisitngCustomer.Id, result.Entity?.Id);
            Assert.Equal(exisitngCustomer.LoginName, result.Entity?.LoginName);
            Assert.Equal(exisitngCustomer.Address, result.Entity?.Address);
            Assert.Equal(exisitngCustomer.Phone, result.Entity?.Phone);
            Assert.True(result.WasOverriden);
            Assert.Equal(1, result.Count);
            mockSet.Verify(x => x.FindAsync(It.IsAny<Guid>()), Times.Once);
            mockSet.Verify(m => m.AddAsync(It.IsAny<Models.Customer>(), default), Times.Never);
            contextMock.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        [Description("Test verifies include data into the database.")]
        public async Task TestUpdateAsync()
        {
            // Arrange
            var newCustomer = new Customer
            {
                Id = new Id<ICustomerId>(Guid.NewGuid()),
                LoginName = "TestLoginName",
                Address = "TestAdress",
                Phone = "TestPhoneNumber"
            };

            var exisitngCustomer = new Models.Customer
            {
                Id = newCustomer.Id,
                LoginName = "OLDLoginName",
                Address = "OLDAdress",
                Phone = "OLDPhoneNumber"
            };

            var logMock = new Mock<ILogger>();

            var mockSet = new Mock<DbSet<ICustomer>>(MockBehavior.Strict);
            mockSet.Setup(m => m.FindAsync(It.IsAny<Guid>())).ReturnsAsync(exisitngCustomer);

            var contextMock = new Mock<ISushiDeliveryContext>(MockBehavior.Strict);
            contextMock.Setup(m => m.GetDbSet<ICustomer>()).Returns(mockSet.Object);
            contextMock.Setup(m => m.SetModified<ICustomer>(exisitngCustomer));
            contextMock.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);

            var customerRepository = new CustomerRepository(
                new Lazy<ISushiDeliveryContext>(contextMock.Object),
                logMock.Object);

            // Act
            var result = await customerRepository.UpdateAsync(newCustomer);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Entity);
            Assert.NotNull(exisitngCustomer);
            Assert.Equal(exisitngCustomer, result.Entity);
            Assert.Equal(exisitngCustomer.Id, result.Entity?.Id);
            Assert.Equal(exisitngCustomer.LoginName, result.Entity?.LoginName);
            Assert.Equal(exisitngCustomer.Address, result.Entity?.Address);
            Assert.Equal(exisitngCustomer.Phone, result.Entity?.Phone);
            Assert.False(result.WasOverriden);
            Assert.Equal(1, result.Count);
            mockSet.Verify(x => x.FindAsync(It.IsAny<Guid>()), Times.Once);
            contextMock.Verify(m => m.SetModified<ICustomer>(exisitngCustomer), Times.Once);
            contextMock.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }
    }
}
