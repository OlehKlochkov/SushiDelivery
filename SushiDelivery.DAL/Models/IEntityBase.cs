using System.ComponentModel.DataAnnotations;

namespace SushiDelivery.DAL.Models
{
    internal interface IEntityBase
    {
        DateTimeOffset CreatedDate
        {
            get => CreatedDate;
            set => CreatedDate = value;
        }

        DateTimeOffset UpdatedDate
        {
            get => UpdatedDate;
            set => UpdatedDate = value;
        }

        DateTimeOffset DeletedDate
        {
            get => DeletedDate;
            set => DeletedDate = value;
        }

        bool IsDeleted
        {
            get => IsDeleted;
            set => IsDeleted = value;
        }

        [Timestamp]
        byte[] TimeStamp
        {
            get => TimeStamp;
            set => TimeStamp = value;
        }
    }
}
