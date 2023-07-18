using SushiDelivery.Domain.Enumerations;
using SushiDelivery.Domain.Models;

namespace SushiDelivery.DAL.Models
{
    internal class Product : Domain.Models.Product, IEntityBase
    {
        public Product() { }

        public Product(IProduct souce)
        {
            Id = souce.Id;
            Name = souce.Name;
            Price = souce.Price;
            IsAvailable = souce.IsAvailable;
            Category = souce.Category;
        }

        public virtual ICollection<ProductIngredient> ProductIngredients { get; set; } = new HashSet<ProductIngredient>();
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedDate { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? DeletedDate { get; set; } = null;
        public bool IsDeleted { get; set; } = false;
        public byte[] TimeStamp { get; set; } = Array.Empty<byte>();

        public Guid GetId() => (Guid)Id;
    }
}
