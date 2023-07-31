namespace SushiDelivery.DAL.Models
{
    /// <summary>
    /// Database - mapped model for the Order domain entity.
    /// </summary>
    internal class Order : Domain.Models.Order, IEntityBase
    {
        // TODO: This implementation is the same for all database - mapped models. Find a way to avoid code duplication.
        #region IEntityBase implementation

        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedDate { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? DeletedDate { get; set; } = null;
        public bool IsDeleted { get; set; } = false;
        public byte[] TimeStamp { get; set; } = Array.Empty<byte>();

        public Guid GetId() => (Guid)Id;

        public override string ToString() => $"{base.ToString()} {nameof(IsDeleted)}={IsDeleted} {nameof(CreatedDate)}={CreatedDate} {nameof(UpdatedDate)}={UpdatedDate} {nameof(DeletedDate)}={DeletedDate} {nameof(TimeStamp)}={TimeStamp}";

        #endregion
    }
}
