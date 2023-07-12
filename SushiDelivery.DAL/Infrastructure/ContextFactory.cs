using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SushiDelivery.DAL.Infrastructure
{
    public class ContextFactory : IContextFactory, IDesignTimeDbContextFactory<SushiDeliveryDbContext>
    {

        #region Constants

        private const string ConfigFileName = "appsettings.json";
        private const string ConnectionStringName = "DefaultConnection";

        #endregion

        private static readonly IConfigurationRoot _configuration 
            = new ConfigurationBuilder().AddJsonFile(ConfigFileName).Build();

        #region Methods

        /// <summary>
        /// Creates a new database context.
        /// </summary>
        /// <returns>Database context.</returns>
        public ISushiDeliveryContext CreateDbContext() => CreateInternal();

        /// <summary>
        /// Creates a new database context.
        /// This method is by Entity Framework tools when creating and applying migrations to the database.
        /// </summary>
        /// <returns>Database context.</returns>
        public SushiDeliveryDbContext CreateDbContext(string[] args) => CreateInternal();

        /// <summary>
        /// Create a new database context.
        /// </summary>
        /// <returns></returns>
        private static SushiDeliveryDbContext CreateInternal()
        {
            var connectionString = GetConnectionString();

            var optionsBuilder = new DbContextOptionsBuilder<SushiDeliveryDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new SushiDeliveryDbContext(optionsBuilder.Options);
        }

        /// <summary>
        /// Gets connection string.
        /// </summary>
        /// <returns>Connection string</returns>
        private static string? GetConnectionString() => _configuration.GetConnectionString(ConnectionStringName);

        #endregion
    }
}
