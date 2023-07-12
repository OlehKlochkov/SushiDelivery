using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.Domain.Models;

namespace SushiDelivery.DAL.Repositories
{
    internal class CustomerRepository : GenericRepository<Models.Customer, Id<ICustomerId>>, ICustomerRepository
    {
        public CustomerRepository(ISushiDeliveryContext context) : base(context)
        {
        }
    }
}
