using SushiDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiDelivery.DAL.Interfaces
{
    public interface IDalMediator
    {
        public IQueryResultGeneric<ICustomer> Query(IQueryGeneric<ICustomer> query);
        public ICreateCommandResult<ICustomerId> Execute(ICreateCommand<ICustomer> customer);
        public ICreateCommandResult<IProductId> Execute(ICreateCommand<IProduct> customer);
    }
}
