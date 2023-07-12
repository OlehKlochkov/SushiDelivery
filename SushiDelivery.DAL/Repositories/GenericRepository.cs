using SushiDelivery.DAL.Infrastructure;

namespace SushiDelivery.DAL.Repositories
{
    /// <summary>
    /// Base repository for database operations.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    internal abstract class GenericRepository<TEntity, TEntityId> : IRepository<TEntity, TEntityId>
            where TEntity : class
            where TEntityId : struct
    {
        protected readonly ISushiDeliveryContext Context;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="contextFactory">Database context.</param>
        protected GenericRepository(ISushiDeliveryContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual async Task<TEntity> GetByIdAsync(TEntityId id)
        {
            var entity = await Context.GetDbSet<TEntity>().FindAsync(id);
            Context.SetDetached(entity);
            return entity;
        }

        public virtual async Task SaveAsync(TEntity entity)
        {
            await Context.GetDbSet<TEntity>().AddAsync(entity);
        }

        public virtual async Task UpdateAsync(TEntity entityToUpdate)
        {
            await Task.Run(() =>
            {
                Context.GetDbSet<TEntity>().Attach(entityToUpdate);
                Context.SetModified(entityToUpdate);
            });
        }
    }
}

