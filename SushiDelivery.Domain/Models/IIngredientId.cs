using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace SushiDelivery.Domain.Models
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