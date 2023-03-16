using System.Linq.Expressions;

namespace SushiDelivery.DAL.Interfaces
{
    public interface IQueryResultGeneric<TEntity>
        where TEntity : class
    {
        public IAsyncEnumerable<TEntity> Entities { get; }
        public int? TotalCount { get; }
    }
}
