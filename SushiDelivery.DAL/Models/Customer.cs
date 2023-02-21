
using SushiDelivery.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SushiDelivery.DAL.Models
{
    public class Customer : Domain.Customer, IEntityBase
    {
    }
}
