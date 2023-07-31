using System.ComponentModel.DataAnnotations.Schema;

namespace SushiDelivery.Domain.Models
{
    /// <summary>
    /// Defines Ingredient entity.
    /// </summary>
    public class Ingredient : IIngredient
    {
        public Id<IIngredientId> Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [NotMapped]
        public ICollection<IProductId> Products { get; set; } = new List<IProductId>();

        public override string ToString() => $"{base.ToString()} {nameof(Id)}={Id} {nameof(Name)}={Name} {nameof(Description)}={Description}";
    }
}
