namespace SushiDelivery.Domain
{
    /// <summary>
    /// Defines ingredient id interface.
    /// </summary>
    public interface IIngredientId
    {
        Id<IIngredientId> Id { get; set; }
    }
}