using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SushiDelivery.Domain
{
    /// <summary>
    /// Defines ingredient id interface.
    /// </summary>
    public interface IIngredientId
    {
        [Key]
        [Required]
        [Editable(false)]
        [Description("IngredientId")]
        [Display(Name = "IngredientId")]
        Id<IIngredientId> Id { get; set; }
    }
}