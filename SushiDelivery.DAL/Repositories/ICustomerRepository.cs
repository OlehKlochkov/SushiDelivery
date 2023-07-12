using SushiDelivery.Domain.Models;

namespace SushiDelivery.DAL.Repositories
{
    interface ICustomerRepository : IRepository<Models.Customer, Id<ICustomerId>>
    {
    }
}
