using SushiDelivery.Domain.Models;

namespace SushiDelivery.DAL.Repositories
{
    public interface ICustomerRepository : IRepository<Models.Customer, Id<ICustomerId>>
    {
    }
}
