namespace SushiDelivery.Domain
{
    /// <summary>
    /// Defines Customer entity.
    /// </summary>
    public class Customer : ICustomer, ICustomerId
    {
        public Id<ICustomerId> Id { get; set; }

        public string LoginName { get; set; }

        public string Address { get; set; }

    }
}