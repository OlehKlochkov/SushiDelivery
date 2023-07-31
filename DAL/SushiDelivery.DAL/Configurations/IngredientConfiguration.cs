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
    internal class IngredientConfiguration : ConfigurationBase, IEntityTypeConfiguration<Models.Ingredient>
    {
        private enum FieldIndex
        {
            Name = 2,
            Description = 3,
        }

        public void Configure(EntityTypeBuilder<Models.Ingredient> entity)
        {
#pragma warning disable IDE0058 // Expression value is never used
            entity.HasKey(e => e.Id).IsClustered(true).HasName("PK__Ingredient_Id");
            entity.Property(e => e.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired()
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .ValueGeneratedOnAdd()
                .HasConversion(ValueConverterHelper<IIngredientId>.GetValueConverter())
                .HasColumnOrder(IdIndex);

            entity.Property(e => e.Name).IsRequired(true).HasColumnOrder((int)FieldIndex.Name);
            entity.Property(e => e.Description).HasColumnOrder((int)FieldIndex.Description);

            entity.HasQueryFilter(o => !((IEntityBase)o).IsDeleted);
#pragma warning restore IDE0058 // Expression value is never used

            ConfigureBase(entity);
        }
    }
}
