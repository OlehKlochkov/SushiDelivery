using System.ComponentModel.DataAnnotations.Schema;

using SushiDelivery.Domain.Enumerations;

namespace SushiDelivery.Domain.Models
{
    /// <summary>
    /// Defines Product entity.
    /// </summary>
    public class Product : IProduct
    {
        public Id<IProductId> Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }

        public Categories Category { get; set; }

        [NotMapped]
        public ICollection<IIngredientId> Ingredients { get; set; } = new List<IIngredientId>();

        public override string ToString() => $"{base.ToString()} {nameof(Id)}={Id} {nameof(Name)}={Name} {nameof(Price)}={Price} {nameof(IsAvailable)}={IsAvailable} {nameof(Category)}={Category}";
    }
}
