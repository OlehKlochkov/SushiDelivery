using Microsoft.EntityFrameworkCore;

namespace SushiDelivery.DAL.Infrastructure
{
    internal class ContextFactory : IContextFactory, IDbContextFactory<SushiDeliveryDbContext>  
    {

        #region Constants

        private const string connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Projects\\SushiDelivery\\SushiDeliveryDb.mdf;Integrated Security = True; Connect Timeout = 30";

        #endregion

        #region _ctors

        /// <summary>
        /// Constructor.
        /// </summary>
        private ContextFactory()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets application wide instance of <see cref="DbContextFactory"/>.
        /// </summary>
        public static ContextFactory Instance { get; } = new ContextFactory();

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
        SushiDeliveryDbContext IDbContextFactory<SushiDeliveryDbContext>.CreateDbContext()
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
