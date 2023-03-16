using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SushiDelivery.DAL.Models;

namespace SushiDelivery.DAL.Configurations
{
    internal class ProductIngredientConfiguration : ConfigurationBase, IEntityTypeConfiguration<ProductIngredient>
    {
        #region Constants

        private const byte ProductIdIndex = 2;
        private const byte IngredientIdIndex = 3;

        #endregion

        public void Configure(EntityTypeBuilder<ProductIngredient> entity)
        {
            entity.HasKey(e => e.Id).IsClustered(true).HasName("PK__ProductIngredient_Id");
            entity.Property(e => e.Id)
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasColumnOrder(IdIndex);

            entity.HasOne(e => e.Product)
                .WithMany(p => p.ProductIngredients)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(e => e.ProductId);

            entity.HasOne(e => e.Ingredient)
                .WithMany(i => i.ProductIngredients)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(e => e.IngredientId);


            entity.Property(e => e.ProductId).HasColumnOrder(ProductIdIndex);
            entity.Property(e => e.IngredientId).HasColumnOrder(IngredientIdIndex);
        }
    }
}
