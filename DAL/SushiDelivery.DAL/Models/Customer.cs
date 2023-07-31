using SushiDelivery.Domain.Models;

namespace SushiDelivery.DAL.Models
{
    /// <summary>
    /// Database - mapped model for the Customer domain entity.
    /// </summary>
    internal class Customer : Domain.Models.Customer, IEntityBase
    {
        public Customer() { }
        public Customer(ICustomer input)
        {
            Id = input.Id;
            LoginName = input.LoginName;
            Address = input.Address;
            Phone = input.Phone;
        }

        // TODO: This implementation is the same for all database - mapped models. Find a way to avoid code duplication.
#region IEntityBase implementation

        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedDate { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? DeletedDate { get; set; } = null;
        public bool IsDeleted { get; set; } = false;
        public byte[] TimeStamp { get; set; } = Array.Empty<byte>();

        public Guid GetId() => (Guid)Id;

 #endregion
    }
}
