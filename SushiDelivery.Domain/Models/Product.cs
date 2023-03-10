using SushiDelivery.Domain.Enumerations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiDelivery.Domain.Models
{
    /// <summary>
    /// Defines Product entity.
    /// </summary>
    public class Product : IProduct
    {
        public Id<IProductId> Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }

        public Categories Category { get; set; }

        [NotMapped]
        public ICollection<IIngredientId> Ingredients { get; set; }
    }
}
