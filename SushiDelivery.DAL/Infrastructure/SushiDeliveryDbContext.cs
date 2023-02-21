using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SushiDelivery.DAL.Models;
using SushiDelivery.Domain;

namespace SushiDelivery.DAL.Infrastructure
{
    /// <summary>
    /// The database context.
    /// </summary>
    internal partial class SushiDeliveryDbContext : DbContext, ISushiDeliveryContext
    {
        public SushiDeliveryDbContext(DbContextOptions<SushiDeliveryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Customer> Customers => Set<Models.Customer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ProcessUpdatedEntities();

            ProcessDeletedEntities();

            return base.SaveChanges();
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        private IEnumerable<IEntityBase> ProcessDeletedEntities()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is IEntityBase && e.State == EntityState.Deleted)
                .Select(o => (IEntityBase)o.Entity);

            foreach (IEntityBase entity in entries)
            {
                entity.DeletedDate = DateTime.UtcNow;
                entity.IsDeleted = true;
            }

            return entries;
        }

        private IEnumerable<IEntityBase> ProcessUpdatedEntities()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is IEntityBase && e.State == EntityState.Modified)
                .Select(o => (IEntityBase)o.Entity);

            foreach (IEntityBase entity in entries)
            {
                entity.UpdatedDate = DateTime.UtcNow;
            }

            return entries;
        }
    }
}