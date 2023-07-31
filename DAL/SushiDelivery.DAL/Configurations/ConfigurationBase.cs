using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SushiDelivery.DAL.Models;

namespace SushiDelivery.DAL.Configurations
{
    /// <summary>
    /// Base mapping for common special fields present in all DB tables.
    /// </summary>
    internal class ConfigurationBase
    {
        protected const byte IdIndex = 1;

        private enum FieldIndex
        {
            CreatedDate = 101,
            UpdatedDate = 102,
            DeletedDateDate = 103,
            IsDeleted = 104,
            TimeStamp = 105
        }

        protected void ConfigureBase(EntityTypeBuilder entity)
        {
            _ = entity.Property<DateTimeOffset>(nameof(IEntityBase.CreatedDate))
                .IsRequired()
                .HasDefaultValueSql("getutcdate()")
                .ValueGeneratedOnAdd()
                .HasColumnOrder((int)FieldIndex.CreatedDate);

            _ = entity.Property<DateTimeOffset>(nameof(IEntityBase.UpdatedDate))
                .IsRequired()
                .HasDefaultValueSql("getutcdate()")
                .HasColumnOrder((int)FieldIndex.UpdatedDate);

            _ = entity.Property<DateTimeOffset?>(nameof(IEntityBase.DeletedDate))
                .IsRequired(false)
                .HasColumnOrder((int)FieldIndex.DeletedDateDate);

            _ = entity.Property<bool>(nameof(IEntityBase.IsDeleted))
                .IsRequired()
                .HasDefaultValueSql("0")
                .HasColumnOrder((int)FieldIndex.IsDeleted);

            _ = entity.Property<byte[]>(nameof(IEntityBase.TimeStamp))
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnOrder((int)FieldIndex.TimeStamp);
        }
    }
}
