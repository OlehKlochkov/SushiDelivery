using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SushiDelivery.DAL.Models;

namespace SushiDelivery.DAL.Configurations
{
    /// <summary>
    /// Enity Framework mapping between the DAL model and the database table.
    /// </summary>
    internal class ProductIngredientConfiguration : ConfigurationBase, IEntityTypeConfiguration<ProductIngredient>
    {
        private enum FieldIndex
        {
            ProductId = 2,
            IngredientId = 3,
        }

        public void Configure(EntityTypeBuilder<ProductIngredient> entity)
        {
            _ = entity.HasKey(e => e.Id).IsClustered(true).HasName("PK__ProductIngredient_Id");

            _ = entity.HasAlternateKey(e => new { e.ProductId, e.IngredientId }).IsClustered(false);

            _ = entity.Property(e => e.Id)
                .IsRequired(true)
                .ValueGeneratedOnAdd()
                .HasColumnOrder(IdIndex);

            _ = entity.HasOne(e => e.Product)
                .WithMany(p => p.ProductIngredients)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(e => e.ProductId);

            _ = entity.HasOne(e => e.Ingredient)
                .WithMany(i => i.ProductIngredients)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(e => e.IngredientId);

            _ = entity.Property(e => e.ProductId).HasColumnOrder((int)FieldIndex.ProductId);

            _ = entity.Property(e => e.IngredientId).HasColumnOrder((int)FieldIndex.IngredientId);
        }
    }
}
