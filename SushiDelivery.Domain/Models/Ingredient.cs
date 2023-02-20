using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SushiDelivery.Domain
{
    /// <summary>
    /// Defines Ingredient entity.
    /// </summary>
    public class Ingredient : IIngredient
    {
        [Key]
        [Required]
        [Editable(false)]
        [Description("IngredientId")]
        [Display(Name = "IngredientId")]
        public Id<IIngredientId> Id { get; set; }

        [Key]
        [Required]
        [MinLength(3)]
        [MaxLength(1024)]
        [StringLength(1024)]
        [Description("IngredientName")]
        [Display(Name = "IngredientName")]
        public string Name { get; set; }

        [MinLength(1)]
        [MaxLength(2048)]
        [StringLength(2048)]
        [Description("IngredientDescription")]
        [Display(Name = "IngredientDescription")]
        public string? Description { get; set; }

        public Ingredient(string name)
        {
            Name = name;
        }
    }
}
