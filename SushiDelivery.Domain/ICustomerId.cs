namespace SushiDelivery.Domain
{
    /// <summary>
    /// Defines customer id interface.
    /// </summary>
    public interface ICustomerId
    {
        Id<ICustomerId> Id { get; set; }
    }
}