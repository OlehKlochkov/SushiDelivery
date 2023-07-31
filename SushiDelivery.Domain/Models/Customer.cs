namespace SushiDelivery.Domain.Models
{
    /// <summary>
    /// Defines Customer entity.
    /// </summary>
    public class Customer : ICustomer, ICustomerId
    {
        public Id<ICustomerId> Id { get; set; }

        public string LoginName { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public override string ToString() => $"{base.ToString()} {nameof(Id)}={Id} {nameof(LoginName)}={LoginName} {nameof(Phone)}={Phone} {nameof(Address)}={Address}";
    }
}
