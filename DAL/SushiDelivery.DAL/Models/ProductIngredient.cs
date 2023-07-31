using SushiDelivery.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace SushiDelivery.DAL.Models
{
    /// <summary>
    /// Database - mapped model for the Product - Ingedient many-to-many mapping.
    /// </summary>
    internal class ProductIngredient
    {
        [Key, Required]
        public int Id { get; set; }

        public Id<IProductId> ProductId { get; set; }

        public virtual Product? Product { get; set; }

        public Id<IIngredientId> IngredientId { get; set; }

        public virtual Ingredient? Ingredient { get; set; }
    }
}
