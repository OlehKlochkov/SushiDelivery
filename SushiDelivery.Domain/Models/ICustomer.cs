using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SushiDelivery.Domain.Models
{
    /// <summary>
    /// Defines customer interface.
    /// </summary>
    public interface ICustomer : ICustomerId
    {
        [Key]
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [MinLength(5)]
        [MaxLength(256)]
        [StringLength(256)]
        [Description("CustomerLoginName")]
        [Display(Name = "CustomerLoginName")]
        string LoginName { get; set; }

        [Phone]
        [DataType(DataType.PhoneNumber)]
        [MinLength(10)]
        [MaxLength(100)]
        [StringLength(100)]
        [Description("CustomerPhone")]
        [Display(Name = "CustomerPhone")]
        string? Phone { get; set; }

        [MinLength(10)]
        [MaxLength(256)]
        [StringLength(256)]
        [Description("CustomerAddress")]
        [Display(Name = "CustomerAddress")]
        string? Address { get; set; }
    }
}