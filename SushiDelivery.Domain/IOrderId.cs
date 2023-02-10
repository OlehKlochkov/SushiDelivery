namespace SushiDelivery.Domain
{
    /// <summary>
    /// Defines order id interface.
    /// </summary>
    public interface IOrderId
    {
        Id<IOrderId> Id { get; set; }
    }
}