namespace SushiDelivery.DAL.Models
{
    internal class Order : Domain.Models.Order, IEntityBase
    {
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedDate { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? DeletedDate { get; set; } = null;
        public bool IsDeleted { get; set; } = false;
        public byte[] TimeStamp { get; set; } = Array.Empty<byte>();

        public Guid GetId() => (Guid)Id;
    }
}
