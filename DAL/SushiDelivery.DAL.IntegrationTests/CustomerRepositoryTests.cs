using System.ComponentModel;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SushiDelivery.DAL.IntegrationTests.Infrastructure;
using SushiDelivery.DAL.Interfaces;
using SushiDelivery.Domain.Models;

using Xunit.Abstractions;

namespace SushiDelivery.DAL.IntegrationTests
{
    public class CustomerRepositoryTests : IntegrationTestBase
    {
        public CustomerRepositoryTests(HostFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {
        }

        [Fact]
        [Description("Test verifies get by id entity from database.")]
        public async Task TestGetCount()
        {
            using (var repository = Services.GetService<ICustomerRepository>()!)
            {
                var result = await repository.Items.CountAsync();
                Assert.True(result >= 0);
            }
        }

        [Fact]
        [Description("Test verifies get by id entity from database.")]
        public async Task TestCreate_MinimalData()
        {
            using (var repository = Services.GetService<ICustomerRepository>()!)
            {
                // Arrange
                ICustomer customer = new Customer()
                {
                    LoginName = GenerateString()
                };

                // Act
                ;
                var result = await repository.CreateAsync(customer);

                // Assert
                AssertOperationResult(result);
                Assert.Equal(customer.LoginName, result?.Entity?.LoginName);

                // Cleanup
                var result1 = await repository.DeleteAsync(result!.Entity!.Id);

                // Assert
                AssertOperationResult(result1);
            }
        }
    }
}
