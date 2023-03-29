using Moq;
using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.DAL.Repositories;
using System.ComponentModel;

namespace SushiDelivery.DAL.Tests.Repositories
{
    public class CustomerRepositoryTests
    {
        private static Models.Customer _expectedCustomer = new Models.Customer
        {
            Id = new Domain.Models.Id<Domain.Models.ICustomerId>(Guid.NewGuid()),
            LoginName = "TestLoginName",
            Address = "TestAdress",
            Phone = "TestPhoneNumber"
        };

        private Mock<ISushiDeliveryContext> _contextMock;
        private CustomerRepository _customerRepository;

        public CustomerRepositoryTests()
        {
            _contextMock = new Mock<ISushiDeliveryContext>();
            _customerRepository = new CustomerRepository(_contextMock.Object);
        }

        [Fact]
        [Description("Test verifies get by id entity from database.")]
        public async void TestGetByIdAsync()
        {
            await GenericRepositoryTests.TestGetByIdAsync(
                _contextMock, _customerRepository, _expectedCustomer, _expectedCustomer.Id);
        }

        [Fact]
        [Description("Test verifies save data into the database.")]
        public async void TestSaveCustomer()
        {
            await GenericRepositoryTests.TestSaveAsync(_contextMock, _customerRepository, _expectedCustomer);
        }

        [Fact]
        [Description("Test verifies include data into the database.")]
        public async void TestIncludeCustomer()
        {
            await GenericRepositoryTests.TestIncludeAsync(_contextMock, _customerRepository, _expectedCustomer);
        }
    }
}
