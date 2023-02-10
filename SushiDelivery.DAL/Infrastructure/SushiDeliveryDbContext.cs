using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SushiDelivery.Domain;

namespace SushiDelivery.DAL.Infrastructure
{
    /// <summary>
    /// The database context.
    /// </summary>
    internal partial class SushiDeliveryDbContext : DbContext, ISushiDeliveryContext
    {
        #region .ctor

        public SushiDeliveryDbContext(DbContextOptions<SushiDeliveryDbContext> options)
            : base(options)
        {
        }

        #endregion

        #region Properties

        public virtual DbSet<Models.Customer> Customers { get; set; }

        #endregion

        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var converter = new ValueConverter<Id<ICustomerId>, Guid>(
              v => (Guid)v,
              v => (Id<ICustomerId>)v);

            modelBuilder.Entity<Models.Customer>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Customer");
                entity.Property(e => e.Id).HasConversion(converter)
                .HasColumnType("uniqueidentifier")
                .IsRequired()
                .ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        #endregion
    }
}