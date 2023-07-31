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
    internal class CustomerConfiguration : ConfigurationBase, IEntityTypeConfiguration<Models.Customer>
    {
        private enum FieldIndex
        {
            LoginName = 2,
            Phone = 3,
            Address = 4,
        }

        public void Configure(EntityTypeBuilder<Models.Customer> entity)
        {
            entity.HasKey(e => e.Id).IsClustered(true).HasName("PK_Customer_Id");
            entity.HasAlternateKey(e => e.LoginName).IsClustered(false);
            entity.Property(e => e.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired()
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .ValueGeneratedOnAdd()
                .HasConversion(ValueConverterHelper<ICustomerId>.GetValueConverter())
                .HasColumnOrder(IdIndex);

            entity.Property(e => e.LoginName)
                .IsRequired(true)
                .HasMaxLength(256)
                .HasColumnOrder((int)FieldIndex.LoginName);
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .HasColumnOrder((int)FieldIndex.Phone);
            entity.Property(e => e.Address)
                .HasMaxLength(256)
                .HasColumnOrder((int)FieldIndex.Address);

            entity.HasQueryFilter(o => !((IEntityBase)o).IsDeleted);

            ConfigureBase(entity);
        }
    }

}
