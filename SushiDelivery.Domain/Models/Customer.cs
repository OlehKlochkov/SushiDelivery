using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SushiDelivery.Domain
{
    /// <summary>
    /// Defines Customer entity.
    /// </summary>
    public class Customer : ICustomer
    {
        [Key]
        [Required]
        [Editable(false)]
        [Description("CustomerId")]
        [Display(Name = "CustomerId")]
        public Id<ICustomerId> Id { get; set; }

        [Key]
        [Required]
        [EmailAddress]
        [MinLength(5)]
        [MaxLength(256)]
        [StringLength(256)]
        [Description("CustomerLoginName")]
        [Display(Name = "CustomerLoginName")]
        public string LoginName { get; set; }

        [Phone]
        [DataType(DataType.PhoneNumber)]
        [MinLength(10)]
        [MaxLength(100)]
        [StringLength(100)]
        [Description("CustomerPhone")]
        [Display(Name = "CustomerPhone")]
        public string? Phone { get; set; }

        [MinLength(10)]
        [MaxLength(256)]
        [StringLength(256)]
        [Description("CustomerAddress")]
        [Display(Name = "CustomerAddress")]
        public string? Address { get; set; }

        public Customer(string loginName)
        {
            LoginName = loginName;
        }

    }
}