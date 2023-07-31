using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SushiDelivery.Domain.Models
{
    /// <summary>
    /// Defines order id interface.
    /// </summary>
    public interface IOrderId
    {
        [Key]
        [Required]
        [Editable(false)]
        [Description("OrderId")]
        [Display(Name = "OrderId")]
        Id<IOrderId> Id { get; set; }
    }
}
