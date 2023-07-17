using SushiDelivery.Domain.Models;

namespace SushiDelivery.DAL.Interfaces
{
    public interface ICustomerRepository : IRepository<ICustomer, ICustomerId>, IQueryEngine<ICustomer>
    {
    }
}
