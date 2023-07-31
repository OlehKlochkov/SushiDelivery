using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using SushiDelivery.Domain.Enumerations;

namespace SushiDelivery.Domain.Models
{
    /// <summary>
    /// Defines product interface.
    /// </summary>
    public interface IProduct : IProductId
    {
        [Required]
        [MinLength(2)]
        [MaxLength(1024)]
        [StringLength(1024)]
        [Description("ProductName")]
        [Display(Name = "ProductName")]
        string Name { get; set; }

        [Required]
        [Range(0.0, 9999.0)]
        [DataType(DataType.Currency)]
        [Description("ProductPrice")]
        [Display(Name = "ProductPrice")]
        decimal Price { get; set; }

        [Required]
        [Description("ProductPrice")]
        [Display(Name = "ProductPrice")]
        bool IsAvailable { get; set; }

        [Required]
        [EnumDataType(typeof(Categories))]
        [Description("ProductCategory")]
        [Display(Name = "ProductCategory")]
        Categories Category { get; set; }

        [NotMapped]
        [Description("ProductIngredients")]
        [Display(Name = "ProductIngredients")]
        ICollection<IIngredientId>? Ingredients { get; }
    }
}
