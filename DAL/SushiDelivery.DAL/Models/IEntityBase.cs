using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiDelivery.DAL.Models
{
    internal interface IEntityBase
    {
        [Required]
        [DataType(DataType.DateTime)]
        [Editable(false)]
        [Description("CreatedDate")]
        [Display(Name = "CreatedDate")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue("getutcdate()")]
        public DateTimeOffset CreatedDate
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.DateTime)]
        [Description("UpdatedDate")]
        [Display(Name = "UpdatedDate")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue("getutcdate()")]
        public DateTimeOffset UpdatedDate
        {
            get;
            set;
        }

        [DataType(DataType.DateTime)]
        [Description("DeletedDate")]
        [Display(Name = "DeletedDate")]
        public DateTimeOffset? DeletedDate
        {
            get;
            set;
        }

        [Required]
        [Description("IsDeleted")]
        [Display(Name = "IsDeleted")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool IsDeleted
        {
            get;
            set;
        }

        [Required]
        [Timestamp]
        [ConcurrencyCheck]
        [Description("TimeStamp")]
        [Display(Name = "TimeStamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public byte[] TimeStamp
        {
            get;
            set;
        }

        public Guid GetId();
    }
}
