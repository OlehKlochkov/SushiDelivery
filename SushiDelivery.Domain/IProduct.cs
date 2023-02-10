using SushiDelivery.Domain.Models.Enumerations;

namespace SushiDelivery.Domain
{
    /// <summary>
    /// Defines product interface.
    /// </summary>
    public interface IProduct : IProductId
    {
        public string Name { get; }

        public double Price { get; }

        public bool IsAvailable { get; }

        public Categories Category { get; }

        ICollection<IIngredientId> Ingredients { get; set; }
    }
}
