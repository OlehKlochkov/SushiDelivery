using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace SushiDelivery.Domain.Models
{
    /// <summary>
    /// Defines ingredient interface.
    /// </summary>
    public interface IIngredient : IIngredientId
    {
        [Key]
        [Required]
        [MinLength(3)]
        [MaxLength(1024)]
        [StringLength(1024)]
        [Description("IngredientName")]
        [Display(Name = "IngredientName")]
        string Name { get; set; }

        [MinLength(1)]
        [MaxLength(2048)]
        [StringLength(2048)]
        [Description("IngredientDescription")]
        [Display(Name = "IngredientDescription")]
        string? Description { get; set; }
    }
}