using SushiDelivery.Domain.Models;

namespace SushiDelivery.DAL.Repositories
{
    internal interface ICustomerRepository : IRepositoryGeneric<Models.Customer, Id<ICustomerId>>
    {
        public Models.Customer? GetByLoginName(string name);
    }
}
