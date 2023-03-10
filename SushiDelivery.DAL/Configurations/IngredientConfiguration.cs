using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.Domain.Models;

namespace SushiDelivery.DAL.Configurations
{
    internal class IngredientConfiguration : ConfigurationBase, IEntityTypeConfiguration<Models.Ingredient>
    {
        #region Constants

        private const byte NameIndex = 2;
        private const byte DescriptionIndex = 3;

        #endregion

        public void Configure(EntityTypeBuilder<Models.Ingredient> entity)
        {
            entity.HasKey(e => e.Id).IsClustered(true).HasName("PK__Ingredient_Id");
            entity.Property(e => e.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired()
                .ValueGeneratedNever()
                .HasConversion(ValueConverterHelper<IIngredientId>.GetValueConverter())
                .HasColumnOrder(IdIndex);

            entity.Property(e => e.Name).IsRequired(true).HasColumnOrder(NameIndex);
            entity.Property(e => e.Description).HasColumnOrder(DescriptionIndex);
            
            ConfigureBase(entity);
        }
    }
}
