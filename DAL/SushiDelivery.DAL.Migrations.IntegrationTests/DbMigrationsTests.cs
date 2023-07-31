using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace SushiDelivery.DAL.Migrations.IntegrationTests
{
    public class DbMigrationsTests
    {

        private readonly ContextFactory _contextFactory;

        public DbMigrationsTests() => _contextFactory = new ContextFactory();

        [Fact]
        [Description("Test verifies that factory creates instance of ISushiDeliveryContext.")]
        public void TestCreate_ISushiDeliveryContext()
        {
            //Act
            using var context = _contextFactory.CreateDbContext();

            //Assert
            Assert.NotNull(context);
        }

        [Fact]
        [Description("Test verifies that factory creates instance of ISushiDeliveryContext.")]
        public async Task CreateDbContext_ApplyMigrations()
        {
            // Act
            using (var context = _contextFactory.CreateDbContext(Array.Empty<string>()))
            {
                // Assert
                Assert.NotNull(context);

                await context.Database.MigrateAsync();

                // Assert
                Assert.Empty(context.Database.GetPendingMigrations());

                // Cleanup
                _ = await context.Database.EnsureDeletedAsync();
            }
        }
    }
}
