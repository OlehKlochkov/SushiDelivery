using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace SushiDelivery.Domain.Models
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