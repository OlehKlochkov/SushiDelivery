﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SushiDelivery.DAL.Migrations
{
    /// <summary>
    /// Context Factory is required by VS and EF toold to generate migrations.
    /// </summary>
    internal class ContextFactory : IDesignTimeDbContextFactory<MigrationsDbContext>
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
        public MigrationsDbContext CreateDbContext() => CreateInternal();

        /// <summary>
        /// Creates a new database context.
        /// This method is by Entity Framework tools when creating and applying migrations to the database.
        /// </summary>
        /// <returns>Database context.</returns>
        public MigrationsDbContext CreateDbContext(string[] args) => CreateInternal();

        /// <summary>
        /// Create a new database context.
        /// </summary>
        /// <returns></returns>
        private static MigrationsDbContext CreateInternal()
        {
            var connectionString = GetConnectionString();

            var optionsBuilder = new DbContextOptionsBuilder<MigrationsDbContext>();
            _ = optionsBuilder.UseSqlServer(connectionString);

            return new MigrationsDbContext(optionsBuilder.Options);
        }

        /// <summary>
        /// Gets connection string.
        /// </summary>
        /// <returns>Connection string</returns>
        private static string? GetConnectionString() => _configuration.GetConnectionString(ConnectionStringName);

        #endregion
    }
}
