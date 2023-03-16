using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiDelivery.DAL.Repositories
{
    internal interface IUnitOfWork
    {
        public ICustomerRepository CustomerRepository { get; }
        public IProductRepository ProductRepository { get; }

        public Task SaveAsync();
    }
}
