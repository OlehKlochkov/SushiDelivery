namespace SushiDelivery.Domain
{
    /// <summary>
    /// Defines ingredient interface.
    /// </summary>
    public interface IIngredient : IIngredientId
    {
        string Name { get; set; }
    }
}