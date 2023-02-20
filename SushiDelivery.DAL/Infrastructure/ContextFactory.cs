using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
using System.Reflection;

namespace SushiDelivery.DAL.Infrastructure
{
    internal class ContextFactory : IContextFactory, IDesignTimeDbContextFactory<SushiDeliveryDbContext>
    {
        private const string ConfigFileName = "appsettings.json";
        private const string ConnectionStringName = "DefaultConnection";

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
            var connectionString = GetConnectionString();

            var optionsBuilder = new DbContextOptionsBuilder<SushiDeliveryDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new SushiDeliveryDbContext(optionsBuilder.Options);
        }

        private static string? GetConnectionString()
        {
            //string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            //UriBuilder uri = new UriBuilder(assemblyLocation);
            //string path = Uri.UnescapeDataString(uri.Path);
            //var codeBase = Path.GetDirectoryName(path);
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile(ConfigFileName)
                .Build();
            return configuration.GetConnectionString(ConnectionStringName);
        }
    }
}
