using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.DAL.Models;
using SushiDelivery.Domain.Models;
using System.ComponentModel;
using Xunit.Abstractions;

namespace SushiDelivery.DAL.Tests.Infrastructure
{
    public class SushiDeliveryDbContextTests : TestBase, IDisposable
    {

        private SushiDeliveryDbContext _context;
        private DbContextOptions<SushiDeliveryDbContext> _dbContextOptions;

        public SushiDeliveryDbContextTests(ITestOutputHelper output)
            : base(output)
        {
            _dbContextOptions = new DbContextOptionsBuilder<SushiDeliveryDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .LogTo(Log.WriteLine)
                .Options;
            _context = new SushiDeliveryDbContext(_dbContextOptions);
        }

        [Fact]
        [Description("Test verifies that all DbSets were initialized.")]
        public void TestDbSets()
        {
            // Assert
            Assert.NotNull(_context.Customers);
            Assert.NotNull(_context.Products);
            Assert.NotNull(_context.Ingredients);
            Assert.NotNull(_context.ProductIngredients);
        }

        [Fact]
        [Description("Test verifies that new Id is generated and returned when entity is added.")]
        public async Task SaveChanges_AddNewRecord()
        {
            var customer = new Models.Customer()
            {
                LoginName = "Test",
                TimeStamp = Array.Empty<byte>()
            };

            await _context.Customers.AddAsync(customer);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            // Assert
            Assert.NotEqual(Guid.Empty, (Guid)customer.Id);

            customer = await _context.Customers.FindAsync(customer.Id);

            Assert.NotNull(customer);
            Assert.True(DateTimeOffset.UtcNow.Subtract(((IEntityBase)customer).CreatedDate).TotalSeconds <= 10);
        }

        [Fact]
        [Description("Test verifies that update date is set when entity is updated.")]
        public async Task SaveChanges_UpdateRecord()
        {
            var utcNow1 = DateTimeOffset.UtcNow;

            var customer1 = new Models.Customer()
            {
                LoginName = "Test",
                TimeStamp = Array.Empty<byte>()
            };

            ((IEntityBase)customer1).CreatedDate = utcNow1;
            ((IEntityBase)customer1).UpdatedDate = utcNow1;

            await _context.Customers.AddAsync(customer1);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            // Assert
            Assert.NotEqual(Guid.Empty, (Guid)customer1.Id);
            Assert.Equal(1, _context.Customers.Where(o => o.Id == customer1.Id).Count());
            Assert.Equal(utcNow1, ((IEntityBase)customer1).CreatedDate);
            Assert.Equal(utcNow1, ((IEntityBase)customer1).UpdatedDate);

            var customer2 = await _context.Customers.FindAsync(customer1.Id);

            // Assert
            Assert.NotNull(customer2);
            Assert.NotEqual(Guid.Empty, (Guid)customer2.Id);
            Assert.Equal(customer1.Id, customer2.Id);

            customer2.Address = "Address";

            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            // Assert
            Assert.NotEqual(((IEntityBase)customer1).UpdatedDate, ((IEntityBase)customer2).UpdatedDate);
            Assert.Equal(1, _context.Customers.Where(o => o.Id == customer2.Id).Count());
            Assert.True(DateTimeOffset.UtcNow.Subtract(((IEntityBase)customer2).UpdatedDate).TotalSeconds <= 10);
        }

        [Fact]
        [Description("Test verifies that deleted date is set when entity is deleted.")]
        public async Task SaveChanges_DeleteRecord()
        {
            var customer1 = new Models.Customer()
            {
                LoginName = "Test",
                TimeStamp = Array.Empty<byte>()
            };

            await _context.Customers.AddAsync(customer1);
            _context.SaveChanges();

            // Assert
            Assert.NotEqual(Guid.Empty, (Guid)customer1.Id);
            Assert.Equal(1, _context.Customers.Where(o => o.Id == customer1.Id).Count());
            _context.ChangeTracker.Clear();

            var customer2 = await _context.Customers.FindAsync(customer1.Id);

            // Assert
            Assert.NotNull(customer2);
            Assert.NotEqual(Guid.Empty, (Guid)customer2.Id);
            Assert.Equal(customer1.Id, customer2.Id);

            _context.Customers.Remove(customer2);

            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            var customer = _context.Customers.SingleOrDefault(o => o.Id == customer2.Id);

            // Assert
            Assert.Null(customer);
            Assert.Equal(0, _context.Customers.Where(o => o.Id == customer2.Id).Count());

            customer = _context.Customers.IgnoreQueryFilters().Single(o => o.Id == customer2.Id);

            // Assert
            Assert.NotNull(customer);
            Assert.True(((IEntityBase)customer).IsDeleted);
            Assert.NotNull(((IEntityBase)customer).DeletedDate);
            Assert.True(DateTimeOffset.UtcNow.Subtract(((IEntityBase)customer).DeletedDate.Value).TotalSeconds <= 10);

            customer = await _context.Customers.FindAsync(customer2.Id);

            // Assert
            Assert.NotNull(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
        }
    }
}
