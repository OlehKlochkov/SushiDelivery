
using SushiDelivery.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SushiDelivery.DAL.Models
{
    internal class Customer : Domain.Customer, IEntityBase
    {
    }

}
