using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SushiDelivery.DAL.Models;

namespace SushiDelivery.DAL.Configurations
{
    internal class ConfigurationBase
    {
        #region Constants

        protected const byte IdIndex = 1;
        private const byte CreatedDateIndex = 101;
        private const byte UpdatedDateIndex = 102;
        private const byte DeletedDateDateIndex = 103;
        private const byte IsDeletedIndex = 104;
        private const byte TimeStampIndex = 105;

        #endregion

        protected void ConfigureBase(EntityTypeBuilder entity)
        {
            entity.Property<DateTimeOffset>(nameof(IEntityBase.CreatedDate))
                .IsRequired()
                .HasDefaultValueSql("getutcdate()")
                .ValueGeneratedOnAdd()
                .HasColumnOrder(CreatedDateIndex);

            entity.Property<DateTimeOffset>(nameof(IEntityBase.UpdatedDate))
                .IsRequired()
                .HasDefaultValueSql("getutcdate()")
                .HasColumnOrder(UpdatedDateIndex);

            entity.Property<DateTimeOffset?>(nameof(IEntityBase.DeletedDate))
                .IsRequired(false)
                .HasColumnOrder(DeletedDateDateIndex);

            entity.Property<bool>(nameof(IEntityBase.IsDeleted))
                .IsRequired()
                .HasDefaultValueSql("0")
                .HasColumnOrder(IsDeletedIndex);

            entity.Property<byte[]>(nameof(IEntityBase.TimeStamp))
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnOrder(TimeStampIndex);
        }
    }
}
