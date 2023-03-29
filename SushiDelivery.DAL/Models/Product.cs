namespace SushiDelivery.DAL.Models
{
    public class Product : Domain.Models.Product, IEntityBase
    {
        public virtual ICollection<ProductIngredient> ProductIngredients { get; set; }
    }
}
