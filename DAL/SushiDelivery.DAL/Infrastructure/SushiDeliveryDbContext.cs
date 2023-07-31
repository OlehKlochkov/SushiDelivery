using System.Reflection.Emit;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

using SushiDelivery.DAL.Configurations;
using SushiDelivery.DAL.Models;

namespace SushiDelivery.DAL.Infrastructure
{
    /// <summary>
    /// The database context.
    /// </summary>
    internal class SushiDeliveryDbContext : DbContext, ISushiDeliveryContext
    {
        #region .ctor

        public SushiDeliveryDbContext(DbContextOptions<SushiDeliveryDbContext> options)
            : base(options)
        {
        }

        protected SushiDeliveryDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected SushiDeliveryDbContext(DbContextOptions options, ILogger logger)
            : base(options) => Log = logger;

        #endregion

        #region Properties
        protected ILogger? Log { get; init; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Ingredient> Ingredients { get; set; }

        public virtual DbSet<ProductIngredient> ProductIngredients { get; set; }


        #endregion

        #region Methods

        /// <summary>
        /// Applying mapping configuration for all DAL Models.
        /// </summary>
        /// <param name="modelBuilder">Model Builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            _ = modelBuilder.ApplyConfiguration(new ProductConfiguration());
            _ = modelBuilder.ApplyConfiguration(new IngredientConfiguration());
            _ = modelBuilder.ApplyConfiguration(new ProductIngredientConfiguration());
        }

        /// <summary>
        /// Get DbSet by entity type.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <returns>One of DbSet properties.</returns>
        public DbSet<TEntity> GetDbSet<TEntity>() 
            where TEntity : class
        {
            using (Log?.BeginScope(nameof(GetDbSet)))
            {
                Log?.LogDebug("TEntity = {TEntity}", typeof(TEntity));

                DbSet<TEntity>? result = null;
                result ??= Customers as DbSet<TEntity>;
                result ??= Products as DbSet<TEntity>;
                result ??= Ingredients as DbSet<TEntity>;
                result ??= ProductIngredients as DbSet<TEntity>;

                Log?.LogDebug("result = {result}", result);

                return result ?? Set<TEntity>();
            }
        }

        public void SetModified<TEntity>(TEntity entity) where TEntity : class => Entry(entity).State = EntityState.Modified;

        public void SetDetached<TEntity>(TEntity entity) where TEntity : class => Entry(entity).State = EntityState.Detached;

        /// <summary>
        /// Override of standard Saving to database.
        /// Implements the following customizations:
        /// - Soft delete (IsDeleted flag)
        /// - Update UpdatedDate and DeletedDate
        /// </summary>
        /// <returns>Number of records affected.</returns>
        public override int SaveChanges()
        {
            using (Log?.BeginScope(nameof(SaveChanges)))
            {
                _ = ProcessUpdatedEntities();

                _ = ProcessDeletedEntities();

                ChangeTracker.DetectChanges();

                var result = base.SaveChanges();

                Log?.LogDebug("result = {result}", result);

                return result;
            }
        }

        /// <summary>
        /// For each record marked for deletion - instead mark it for update
        /// and set IsDeleted = true.
        /// </summary>
        /// <returns>Soft-deleted entities.</returns>
        private IEnumerable<IEntityBase> ProcessDeletedEntities()
        {
            using (Log?.BeginScope(nameof(ProcessDeletedEntities)))
            {
                var entries = ChangeTracker
                    .Entries()
                    .Where(e => e.Entity is IEntityBase && e.State == EntityState.Deleted)
                    .Select(o => o)
                    .ToArray();

                Log?.LogDebug("entries = {entries}", entries?.Length);

                foreach (var entity in entries ?? Array.Empty<EntityEntry>())
                {
                    entity.State = EntityState.Modified;
                    ((IEntityBase)entity.Entity).DeletedDate = DateTimeOffset.UtcNow;
                    ((IEntityBase)entity.Entity).IsDeleted = true;

                    Log?.LogDebug("Entity = {Entity}", entity.Entity);
                }

                return entries?.Select(o => (IEntityBase)o.Entity)
                    ?? Enumerable.Empty<IEntityBase>();
            }
        }

        /// <summary>
        /// For each updated record also set UpdatedDate to UtcNow.
        /// </summary>
        /// <returns>Updated records.</returns>
        private IEnumerable<IEntityBase> ProcessUpdatedEntities()
        {
            using (Log?.BeginScope(nameof(ProcessUpdatedEntities)))
            {
                var entries = ChangeTracker
                    .Entries()
                    .Where(e => e.Entity is IEntityBase && e.State == EntityState.Modified)
                    .Select(o => (IEntityBase)o.Entity)
                    .ToArray();

                Log?.LogDebug("entries = {entries}", entries?.Length);

                foreach (var entity in entries ?? Array.Empty<IEntityBase>())
                {
                    entity.UpdatedDate = DateTimeOffset.UtcNow;
                    entity.IsDeleted = false;
                    entity.DeletedDate = null;

                    Log?.LogDebug("Entity = {Entity}", entity);
                }

                return entries ?? Enumerable.Empty<IEntityBase>();
            }
        }

        #endregion
    }
}
