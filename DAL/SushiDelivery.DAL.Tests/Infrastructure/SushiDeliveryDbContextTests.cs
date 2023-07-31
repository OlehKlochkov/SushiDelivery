using Microsoft.EntityFrameworkCore;

using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.DAL.Models;

using System.ComponentModel;

using Xunit.Abstractions;

namespace SushiDelivery.DAL.Tests.Infrastructure
{
    public class SushiDeliveryDbContextTests : TestBase, IDisposable
    {
        public SushiDeliveryDbContextTests(ITestOutputHelper output)
            : base(output)
        {
        }

        private ISushiDeliveryContext CreateSushiDeliveryContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<SushiDeliveryDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .LogTo(Log.WriteLine)
                .Options;
            var context = new SushiDeliveryDbContext(dbContextOptions);
            return context;
        }

        [Fact]
        [Description("Test verifies that all DbSets were initialized.")]
        public void EmplyDbContextTest()
        {
            // Act
            using (var context = CreateSushiDeliveryContext())
            {
                // Assert
                Assert.NotNull(context.Customers);
                Assert.NotNull(context.Products);
                Assert.NotNull(context.Ingredients);
                Assert.Equal(0, context.Customers.Count());
                Assert.Equal(0, context.Products.Count());
                Assert.Equal(0, context.Ingredients.Count());
            }
        }

        [Fact]
        [Description("Test verifies GetDbSet method.")]
        public void GetDbSet_DALModels_Test()
        {
            // Act
            using (var context = CreateSushiDeliveryContext())
            {
                var customers = context.GetDbSet<Models.Customer>();
                var products = context.GetDbSet<Models.Product>();
                var ingredients = context.GetDbSet<Models.Ingredient>();
                // Assert
                Assert.NotNull(customers);
                Assert.NotNull(products);
                Assert.NotNull(ingredients);
                Assert.Equal(context.Customers, customers);
                Assert.Equal(context.Products, products);
                Assert.Equal(context.Ingredients, ingredients);
            }
        }

        [Fact]
        [Description("Test verifies that new Id is generated and returned when entity is added.")]
        public async Task SaveChanges_AddNewRecord()
        {
            // Arrange
            using (var context = CreateSushiDeliveryContext())
            {
                var customer = new Models.Customer()
                {
                    LoginName = "Test"
                };

                _ = await context.Customers.AddAsync(customer);

                Assert.Equal(0, await context.Customers.CountAsync());

                var result = await context.SaveChangesAsync();

                Assert.Equal(1, result);

                ((DbContext)context).ChangeTracker.Clear();

                Assert.Equal(1, await context.Customers.CountAsync());

                // Assert
                Assert.NotEqual(Guid.Empty, (Guid)customer.Id);

                customer = await context.Customers.FindAsync(customer.Id);

                Assert.NotNull(customer);

                Log.WriteLine(customer.ToString());

                Assert.True(DateTimeOffset.UtcNow.Subtract(((IEntityBase)customer).CreatedDate).TotalSeconds <= 10);
            }
        }

        [Fact]
        [Description("Test verifies that update date is set when entity is updated.")]
        public async Task SaveChanges_UpdateRecord()
        {
            // Arrange
            using (var context = CreateSushiDeliveryContext())
            {
                var utcNow1 = DateTimeOffset.UtcNow;

                var customer1 = new Models.Customer
                {
                    LoginName = "Test",
                    CreatedDate = utcNow1,
                    UpdatedDate = utcNow1
                };

                _ = await context.Customers.AddAsync(customer1);

                var result = await context.SaveChangesAsync();

                Assert.Equal(1, result);

                Log.WriteLine(customer1.ToString());

                ((DbContext)context).ChangeTracker.Clear();

                // Assert
                Assert.Equal(1, await context.Customers.CountAsync());
                Assert.NotEqual(Guid.Empty, (Guid)customer1.Id);
                Assert.Equal(1, await context.Customers.Where(o => o.Id == customer1.Id).CountAsync());

                var customer2 = await context.Customers.FindAsync(customer1.Id);

                // Assert
                Assert.NotNull(customer2);
                Assert.NotEqual(Guid.Empty, (Guid)customer2.Id);
                Assert.Equal(customer1.Id, customer2.Id);

                Log.WriteLine(customer2.ToString());

                // Act
                customer2.Address = "Address";

                result = await context.SaveChangesAsync();
                Assert.Equal(1, result);

                ((DbContext)context).ChangeTracker.Clear();

                // Assert
                Assert.NotEqual(customer1.UpdatedDate, customer2.UpdatedDate);
                Assert.Equal(1, await context.Customers.Where(o => o.Id == customer2.Id).CountAsync());

                var customer3 = await context.Customers.FindAsync(customer1.Id);

                // Assert
                Assert.NotNull(customer3);
                Assert.NotEqual(Guid.Empty, (Guid)customer3.Id);
                Assert.Equal(customer1.Id, customer3.Id);
                Assert.True(DateTimeOffset.UtcNow.Subtract(((IEntityBase)customer3).UpdatedDate).TotalSeconds <= 10);

                Log.WriteLine(customer3.ToString());
            }
        }

        [Fact]
        [Description("Test verifies that deleted date is set when entity is deleted.")]
        public async Task SaveChanges_DeleteRecord()
        {
            using (var context = CreateSushiDeliveryContext())
            {
                var customer1 = new Models.Customer()
                {
                    LoginName = "Test"
                };

                _ = await context.Customers.AddAsync(customer1);

                var result = await context.SaveChangesAsync();

                Assert.Equal(1, result);

                Log.WriteLine(customer1.ToString());

                ((DbContext)context).ChangeTracker.Clear();

                // Assert
                Assert.NotEqual(Guid.Empty, (Guid)customer1.Id);
                Assert.Equal(1, context.Customers.Where(o => o.Id == customer1.Id).Count());

                var customer2 = await context.Customers.FindAsync(customer1.Id);

                // Assert
                Assert.NotNull(customer2);
                Assert.NotEqual(Guid.Empty, (Guid)customer2.Id);
                Assert.Equal(customer1.Id, customer2.Id);

                Log.WriteLine(customer2.ToString());

                // Act
                _ = context.Customers.Remove(customer2);

                result = await context.SaveChangesAsync();

                Assert.Equal(1, result);

                ((DbContext)context).ChangeTracker.Clear();

                var customer = context.Customers.SingleOrDefault(o => o.Id == customer2.Id);

                // Assert
                Assert.Null(customer);
                Assert.Equal(0, context.Customers.Where(o => o.Id == customer2.Id).Count());

                customer = context.Customers.IgnoreQueryFilters().Single(o => o.Id == customer2.Id);

                // Assert
                Assert.NotNull(customer);
                Assert.True(((IEntityBase)customer).IsDeleted);
                Assert.NotNull(((IEntityBase)customer).DeletedDate);
                Assert.True(DateTimeOffset.UtcNow.Subtract(((IEntityBase)customer).DeletedDate.Value).TotalSeconds <= 10);

                Log.WriteLine(customer.ToString());

                customer = await context.Customers.FindAsync(customer2.Id);

                // Assert
                Assert.NotNull(customer);
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                using (var context = CreateSushiDeliveryContext())
                {
                    // Ensue database is re-created for every test.
                    ((SushiDeliveryDbContext)context).Database.EnsureDeleted();
                }
            }
            base.Dispose(disposing);
        }
    }
}
