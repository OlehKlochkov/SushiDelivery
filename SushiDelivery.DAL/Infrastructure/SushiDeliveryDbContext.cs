using Microsoft.EntityFrameworkCore;
using SushiDelivery.Domain.Models;

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
            modelBuilder.Entity<Models.Customer>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Customer");
                entity.Property(e => e.Id).HasConversion(ValueConverterHelper<ICustomerId>.GetValueConverter())
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