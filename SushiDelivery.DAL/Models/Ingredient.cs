namespace SushiDelivery.DAL.Models
{
    internal class Ingredient : Domain.Models.Ingredient, IEntityBase
    {
        public virtual ICollection<ProductIngredient> ProductIngredients { get; set; }
    }
}
