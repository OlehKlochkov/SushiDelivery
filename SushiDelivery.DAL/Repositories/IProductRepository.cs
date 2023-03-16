using SushiDelivery.Domain.Models;

namespace SushiDelivery.DAL.Repositories
{
    internal interface IProductRepository : IRepositoryGeneric<Models.Product, Id<IProductId>>
    {
        public IAsyncEnumerable<Models.Product> SearchByName(string name);
    }
}
