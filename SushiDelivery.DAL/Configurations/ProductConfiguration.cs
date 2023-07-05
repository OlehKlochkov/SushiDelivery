using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.Domain.Models;
using SushiDelivery.DAL.Models;

namespace SushiDelivery.DAL.Configurations
{
    internal class ProductConfiguration : ConfigurationBase, IEntityTypeConfiguration<Models.Product>
    {
        #region Constants

        private const byte NameIndex = 2;
        private const byte PriceIndex = 3;
        private const byte IsAvailableIndex = 4;
        private const byte CategoryIndex = 5;

        #endregion

        public void Configure(EntityTypeBuilder<Models.Product> entity)
        {
            entity.HasKey(e => e.Id).IsClustered(true).HasName("PK__Product_Id");
            entity.Property(e => e.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired()
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .ValueGeneratedOnAdd()
                .HasConversion(ValueConverterHelper<IProductId>.GetValueConverter())
                .HasColumnOrder(IdIndex);

            entity.Property(e => e.Name).IsRequired(true).HasColumnOrder(NameIndex);
            entity.Property(e => e.Price).HasColumnOrder(PriceIndex);
            entity.Property(e => e.IsAvailable).HasColumnOrder(IsAvailableIndex);
            entity.Property(e => e.Category).HasColumnOrder(CategoryIndex);

            entity.HasQueryFilter(o => !((IEntityBase)o).IsDeleted);

            ConfigureBase(entity);
        }
    }
}
