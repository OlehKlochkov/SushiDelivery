using SushiDelivery.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace SushiDelivery.DAL.Models
{
    public class ProductIngredient
    {
        [Key, Required]
        public int Id { get; set; }

        public Id<IProductId> ProductId { get; set; }

        public virtual Product? Product { get; set; }

        public Id<IIngredientId> IngredientId { get; set; }

        public virtual Ingredient? Ingredient { get; set; }
    }
}
