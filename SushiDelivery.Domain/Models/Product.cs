using SushiDelivery.Domain.Models.Enumerations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SushiDelivery.Domain
{
    /// <summary>
    /// Defines Product entity.
    /// </summary>
    public class Product : IProduct
    {
        [Key]
        [Required]
        [Editable(false)]
        [Description("ProductId")]
        [Display(Name = "ProductId")]
        public Id<IProductId> Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(1024)]
        [StringLength(1024)]
        [Description("ProductName")]
        [Display(Name = "ProductName")]
        public string Name { get; set; }

        [Required]
        [Range(0.0, 9999.0)]
        [DataType(DataType.Currency)]
        [Description("ProductPrice")]
        [Display(Name = "ProductPrice")]
        public decimal Price { get; set; }

        [Required]
        [Description("ProductPrice")]
        [Display(Name = "ProductPrice")]
        public bool IsAvailable { get; set; }

        [Required]
        [EnumDataType(typeof(Categories))]
        [Description("ProductCategory")]
        [Display(Name = "ProductCategory")]
        public Categories Category { get; set; }

        [Description("ProductIngredients")]
        [Display(Name = "ProductIngredients")]
        public ICollection<IIngredientId> Ingredients { get; } = new List<IIngredientId>();

        public Product(string name)
        {
            Name = name;
        }
    }
}
