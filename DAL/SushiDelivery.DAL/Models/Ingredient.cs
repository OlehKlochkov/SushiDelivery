namespace SushiDelivery.DAL.Models
{
    /// <summary>
    /// Database-mapped model for the Ingredient entity.
    /// </summary>
    internal class Ingredient : Domain.Models.Ingredient, IEntityBase
    {
        public virtual ICollection<ProductIngredient> ProductIngredients { get; set; } = new HashSet<ProductIngredient>();

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
