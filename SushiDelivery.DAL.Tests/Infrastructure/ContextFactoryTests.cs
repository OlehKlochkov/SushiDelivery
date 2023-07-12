using Microsoft.EntityFrameworkCore;
using SushiDelivery.DAL.Infrastructure;
using System.ComponentModel;

namespace SushiDelivery.DAL.Tests.Infrastructure
{
    public class ContextFactoryTests
    {

        private readonly ContextFactory _contextFactory;

        public ContextFactoryTests() => _contextFactory = new ContextFactory();

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
        public void CreateDbContext_ApplyMigrations()
        {
            //Act
            using (var context = _contextFactory.CreateDbContext(new string[] { }))
            {
                //Assert
                Assert.NotNull(context);

                context.Database.Migrate();

                //Assert
                Assert.Empty(context.Database.GetPendingMigrations());
            }
        }
    }
}
