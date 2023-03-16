using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

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