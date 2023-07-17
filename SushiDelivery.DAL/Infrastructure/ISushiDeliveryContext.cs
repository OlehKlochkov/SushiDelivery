using Microsoft.EntityFrameworkCore;

namespace SushiDelivery.DAL.Infrastructure
{
    /// <summary>
    /// Interface for the database context.
    /// </summary>
    internal interface ISushiDeliveryContext : IDisposable
    {
        #region Properties

        DbSet<Models.Customer> Customers { get; set; }

        DbSet<Models.Product> Products { get; set; }

        DbSet<Models.Ingredient> Ingredients { get; set; }

        #endregion

        #region Methods

        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;

        void SetModified<TEntity>(TEntity entity) where TEntity : class;

        void SetDetached<TEntity>(TEntity entity) where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        #endregion
    }
}
