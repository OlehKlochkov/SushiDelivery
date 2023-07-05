using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.Domain.Models;

namespace SushiDelivery.DAL.Repositories
{
    public class CustomerRepository : GenericRepository<Models.Customer, Id<ICustomerId>>, ICustomerRepository
    {
        public CustomerRepository(ISushiDeliveryContext context) : base(context)
        {
        }
    }
}
