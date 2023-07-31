using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SushiDelivery.Domain.Models
{
    /// <summary>
    /// Defines customer id interface.
    /// </summary>
    public interface ICustomerId
    {
        [Key]
        [Required]
        [Editable(false)]
        [Description("CustomerId")]
        [Display(Name = "CustomerId")]
        Id<ICustomerId> Id { get; set; }
    }
}
