namespace SushiDelivery.Domain.Models
{
    /// <summary>
    /// Defines Order entity.
    /// </summary>
    public class Order : IOrder
    {
        public Id<IOrderId> Id { get; set; }

        public IList<IProductId> Products { get; set; }

        public ICustomerId CustomerId { get; set; }
    }
}
