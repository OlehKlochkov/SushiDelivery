using System.Linq.Expressions;

namespace SushiDelivery.DAL.Interfaces
{
    public interface IQueryGeneric<TEntity>
        where TEntity : class
    {
        Expression<Func<TEntity, bool>> Filter { get; }
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; }
        int? Skip { get; }
        int? Take { get; }
    }
}
