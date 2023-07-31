namespace SushiDelivery.Domain.Models
{
    /// <summary>
    /// Defines Order entity.
    /// </summary>
    public class Order : IOrder
    {
        public Id<IOrderId> Id { get; set; }

        public IList<IProductId> Products { get; set; } = new List<IProductId>();

        public ICustomerId CustomerId { get; set; } = null!;

        public override string ToString() => $"{base.ToString()} {nameof(Id)}={Id} {nameof(CustomerId)}={CustomerId}";
    }
}
