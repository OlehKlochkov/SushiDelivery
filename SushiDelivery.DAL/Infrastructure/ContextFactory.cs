using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SushiDelivery.DAL.Infrastructure
{
    internal class ContextFactory : IContextFactory, IDesignTimeDbContextFactory<SushiDeliveryDbContext>
    {

        #region Constants

        private const string connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Projects\\SushiDelivery\\SushiDeliveryDb.mdf;Integrated Security = True; Connect Timeout = 30";

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new database context.
        /// </summary>
        /// <returns>Database context.</returns>
        public ISushiDeliveryContext CreateDbContext()
        {
            return CreateInternal();
        }

        /// <summary>
        /// Creates a new database context.
        /// This method is by Entity Framework tools when creating and applying migrations to the database.
        /// </summary>
        /// <returns>Database context.</returns>
        public SushiDeliveryDbContext CreateDbContext(string[] args)
        {
            return CreateInternal();
        }

        /// <summary>
        /// Create a new database context.
        /// </summary>
        /// <returns></returns>
        private static SushiDeliveryDbContext CreateInternal()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SushiDeliveryDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new SushiDeliveryDbContext(optionsBuilder.Options);
        }

        #endregion
    }
}
