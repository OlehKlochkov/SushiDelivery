using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SushiDelivery.DAL.Models
{
    public interface IEntityBase
    {
        [Required]
        [DataType(DataType.DateTime)]
        [Editable(false)]
        [Description("CreatedDate")]
        [Display(Name = "CreatedDate")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue("getutcdate()")]
        public DateTimeOffset CreatedDate
        {
            get => CreatedDate;
            set => CreatedDate = value;
        }

        [Required]
        [DataType(DataType.DateTime)]
        [Description("UpdatedDate")]
        [Display(Name = "UpdatedDate")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue("getutcdate()")]
        public DateTimeOffset UpdatedDate
        {
            get => UpdatedDate;
            set => UpdatedDate = value;
        }

        [DataType(DataType.DateTime)]
        [Description("DeletedDate")]
        [Display(Name = "DeletedDate")]
        public DateTimeOffset? DeletedDate
        {
            get => DeletedDate;
            set => DeletedDate = value;
        }

        [Required]
        [Description("IsDeleted")]
        [Display(Name = "IsDeleted")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue("false")]
        public bool IsDeleted
        {
            get => IsDeleted;
            set => IsDeleted = value;
        }

        [Required]
        [Timestamp]
        [ConcurrencyCheck]
        [Description("TimeStamp")]
        [Display(Name = "TimeStamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public byte[] TimeStamp
        {
            get => TimeStamp;
            set => TimeStamp = value;
        }
    }
}
