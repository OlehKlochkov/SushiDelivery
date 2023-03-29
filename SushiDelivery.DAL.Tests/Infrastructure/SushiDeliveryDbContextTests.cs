using Microsoft.EntityFrameworkCore;
using SushiDelivery.DAL.Infrastructure;
using System.ComponentModel;

namespace SushiDelivery.DAL.Tests.Infrastructure
{
    public class SushiDeliveryDbContextTests : IDisposable
    {

        private SushiDeliveryDbContext _context;
        private DbContextOptions<SushiDeliveryDbContext> _dbContextOptions;

        public SushiDeliveryDbContextTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<SushiDeliveryDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
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

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}
