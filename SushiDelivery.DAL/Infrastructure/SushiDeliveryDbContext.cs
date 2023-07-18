using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
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

        public SushiDeliveryDbContext(DbContextOptions options)
            : base(options)
        {
        }

        #endregion

        #region Properties

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Ingredient> Ingredients { get; set; }

        public virtual DbSet<ProductIngredient> ProductIngredients { get; set; }


        #endregion

        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            _ = modelBuilder.ApplyConfiguration(new ProductConfiguration());
            _ = modelBuilder.ApplyConfiguration(new IngredientConfiguration());
            _ = modelBuilder.ApplyConfiguration(new ProductIngredientConfiguration());
        }

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class => Set<TEntity>();

        public void SetModified<TEntity>(TEntity entity) where TEntity : class => Entry(entity).State = EntityState.Modified;

        public void SetDetached<TEntity>(TEntity entity) where TEntity : class => Entry(entity).State = EntityState.Detached;

        public override int SaveChanges()
        {
            _ = ProcessUpdatedEntities();

            _ = ProcessDeletedEntities();

            ChangeTracker.DetectChanges();

            return base.SaveChanges();
        }

        private IEnumerable<IEntityBase> ProcessDeletedEntities()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is IEntityBase && e.State == EntityState.Deleted)
                .Select(o => o)
                .ToArray();

            foreach (var entity in entries)
            {
                entity.State = EntityState.Modified;
                ((IEntityBase)entity.Entity).DeletedDate = DateTimeOffset.UtcNow;
                ((IEntityBase)entity.Entity).IsDeleted = true;
            }

            return entries.Select(o => (IEntityBase)o.Entity);
        }

        private IEnumerable<IEntityBase> ProcessUpdatedEntities()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is IEntityBase && e.State == EntityState.Modified)
                .Select(o => (IEntityBase)o.Entity)
                .ToArray();

            foreach (var entity in entries)
            {
                entity.UpdatedDate = DateTimeOffset.UtcNow;
            }

            return entries;
        }

        #endregion
    }
}
