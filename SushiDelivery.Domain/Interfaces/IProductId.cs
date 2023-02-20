using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SushiDelivery.Domain
{
    /// <summary>
    /// Defines product id interface.
    /// </summary>
    public interface IProductId
    {
        [Key]
        [Required]
        [Editable(false)]
        [Description("ProductId")]
        [Display(Name = "ProductId")]
        Id<IProductId> Id { get; set; }
    }
}