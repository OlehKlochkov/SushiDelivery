using SushiDelivery.Domain.Models;

namespace SushiDelivery.DAL.Repositories
{
    interface IProductRepository : IRepository<Models.Product, Id<IProductId>>
    {
        Task<IEnumerable<Models.Product>> SearchByNameAsync(string name);
    }
}
