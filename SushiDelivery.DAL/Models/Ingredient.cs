namespace SushiDelivery.DAL.Models
{
    public class Ingredient : Domain.Models.Ingredient, IEntityBase
    {
        public virtual ICollection<ProductIngredient> ProductIngredients { get; set; }
    }
}
