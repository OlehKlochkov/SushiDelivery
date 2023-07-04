namespace SushiDelivery.DAL.Models
{
    public class Product : Domain.Models.Product, IEntityBase
    {
        public virtual ICollection<ProductIngredient> ProductIngredients { get; set; } = new HashSet<ProductIngredient>();
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedDate { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? DeletedDate { get; set; } = null;
        public bool IsDeleted { get; set; } = false;
        public byte[] TimeStamp { get; set; }
    }
}
