using Microsoft.Extensions.Logging;

using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.DAL.Interfaces;
using SushiDelivery.Domain.Models;

namespace SushiDelivery.DAL.Repositories
{
    internal class CustomerRepository : GenericRepository<ICustomer, ICustomerId>, ICustomerRepository
    {
        public CustomerRepository(Lazy<ISushiDeliveryContext> lazyContext, ILogger logger, bool autoSaveChanges = true)
            : base(lazyContext, logger, autoSaveChanges)
        {
        }

        public IQueryable<ICustomer> Items => Context.Customers;

        protected override ICustomer Create(ICustomer entityIn) => new Models.Customer(entityIn);

        protected override void Map(ICustomer entityOut, ICustomer entityIn)
        {
            entityOut.Address = entityIn.Address;
            entityOut.Phone = entityIn.Phone;
        }
    }
}
