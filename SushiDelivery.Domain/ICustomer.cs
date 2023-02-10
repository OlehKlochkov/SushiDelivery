namespace SushiDelivery.Domain
{
    /// <summary>
    /// Defines customer interface.
    /// </summary>
    public interface ICustomer : ICustomerId
    {
        string LoginName { get; set; }

        string Address { get; set; }
    }
}