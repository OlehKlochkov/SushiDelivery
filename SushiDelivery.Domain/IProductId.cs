namespace SushiDelivery.Domain
{
    /// <summary>
    /// Defines product id interface.
    /// </summary>
    public interface IProductId
    {
        Id<IProductId> Id { get; set; }
    }
}