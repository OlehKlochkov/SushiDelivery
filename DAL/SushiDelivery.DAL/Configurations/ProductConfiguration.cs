using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.Domain.Models;
using SushiDelivery.DAL.Models;

namespace SushiDelivery.DAL.Configurations
{
    /// <summary>
    /// Enity Framework mapping between the DAL model and the database table.
    /// </summary>
    internal class ProductConfiguration : ConfigurationBase, IEntityTypeConfiguration<Models.Product>
    {
        private enum FieldIndex
        {
            Name = 2,
            Price = 3,
            IsAvailable = 4,
            Category = 5,
        }

        public void Configure(EntityTypeBuilder<Models.Product> entity)
        {
#pragma warning disable IDE0058 // Expression value is never used
            entity.HasKey(e => e.Id).IsClustered(true).HasName("PK__Product_Id");
            entity.Property(e => e.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired()
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .ValueGeneratedOnAdd()
                .HasConversion(ValueConverterHelper<IProductId>.GetValueConverter())
                .HasColumnOrder(IdIndex);

            entity.Property(e => e.Name).IsRequired(true).HasColumnOrder((int)FieldIndex.Name);
            entity.Property(e => e.Price).HasColumnOrder((int)FieldIndex.Price);
            entity.Property(e => e.IsAvailable).HasColumnOrder((int)FieldIndex.IsAvailable);
            entity.Property(e => e.Category).HasColumnOrder((int)FieldIndex.Category);

            entity.HasQueryFilter(o => !((IEntityBase)o).IsDeleted);
#pragma warning restore IDE0058 // Expression value is never used

            ConfigureBase(entity);
        }
    }
}
