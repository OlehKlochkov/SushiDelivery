namespace SushiDelivery.DAL.Models
{
    internal class Product : Domain.Models.Product, IEntityBase
    {
        public virtual ICollection<ProductIngredient> ProductIngredients { get; set; }
    }
}
