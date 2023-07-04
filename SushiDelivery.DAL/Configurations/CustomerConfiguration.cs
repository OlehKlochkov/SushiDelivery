using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.Domain.Models;
using SushiDelivery.DAL.Models;

namespace SushiDelivery.DAL.Configurations
{
    internal class CustomerConfiguration : ConfigurationBase, IEntityTypeConfiguration<Models.Customer>
    {
        #region Constants

        private const byte LoginNameIndex = 2;
        private const byte PhoneIndex = 3;
        private const byte AddressIndex = 4;

        #endregion

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
                .HasColumnOrder(LoginNameIndex);
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .HasColumnOrder(PhoneIndex);
            entity.Property(e => e.Address)
                .HasMaxLength(256)
                .HasColumnOrder(AddressIndex);

            entity.HasQueryFilter(o => !((IEntityBase)o).IsDeleted);

            ConfigureBase(entity);
        }
    }

}
