namespace SushiDelivery.Domain
{
    /// <summary>
    /// Defines order interface.
    /// </summary>
    public interface IOrder : IOrderId
    {
        IList<IProductId> Products { get; set; }
        ICustomerId Customer { get; set; }
    }
}