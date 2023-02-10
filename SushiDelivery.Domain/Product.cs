using SushiDelivery.Domain.Models.Enumerations;

namespace SushiDelivery.Domain
{
    /// <summary>
    /// Defines Product entity.
    /// </summary>
    public class Product : IProduct
    {
        public Id<IProductId> Id { get; set; }

        public string Name { get; }

        public double Price { get; }

        public bool IsAvailable { get; }

        public Categories Category { get; }

        public ICollection<IIngredientId> Ingredients { get; set; }
    }
}
