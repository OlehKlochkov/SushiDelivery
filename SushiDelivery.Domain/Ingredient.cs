namespace SushiDelivery.Domain
{
    /// <summary>
    /// Defines Ingredient entity.
    /// </summary>
    public class Ingredient : IIngredient
    {
        public Id<IIngredientId> Id { get; set; }

        public string Name { get; set; }
    }
}
