namespace SushiDelivery.Domain.Models
{
    /// <summary>
    /// Defines order interface.
    /// </summary>
    public interface IOrder : IOrderId
    {
        IList<IProductId> Products { get; set; }

        ICustomerId CustomerId { get; set; }
    }
}