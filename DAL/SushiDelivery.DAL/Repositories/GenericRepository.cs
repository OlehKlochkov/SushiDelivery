using Microsoft.Extensions.Logging;

using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.DAL.Interfaces;
using SushiDelivery.DAL.Models;
using SushiDelivery.Domain.Models;

namespace SushiDelivery.DAL.Repositories
{
    /// <summary>
    /// Base repository for database operations.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity.</typeparam>
    internal abstract class GenericRepository<TEntityModel, TEntity, TEntityId> : IRepository<TEntity, TEntityId>
        where TEntityId : class
        where TEntity : class, TEntityId
        where TEntityModel : class, TEntity
    {
        /// <summary>
        /// Flag that indicates whether object was disposed. 
        /// </summary>
        private bool _isDisposed;

        protected ILogger Log { get; }

        private readonly Lazy<ISushiDeliveryContext> _lazyContext;
        protected ISushiDeliveryContext Context => _lazyContext.Value;

        public bool AutoSaveChanges { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="contextFactory">Database context.</param>
        protected GenericRepository(Lazy<ISushiDeliveryContext> lazyContext, ILogger logger, bool autoSaveChanges = true)
        {
            _lazyContext = lazyContext ?? throw new ArgumentNullException(nameof(lazyContext));
            Log = logger ?? throw new ArgumentNullException(nameof(logger));
            AutoSaveChanges = autoSaveChanges;
        }

        protected abstract TEntityModel Create(TEntity entityIn);
        protected abstract void Map(TEntityModel entityOut, TEntity entityIn);

        public virtual async Task<TEntity?> GetByIdAsync(Id<TEntityId> id)
        {
            using (Log.BeginScope(nameof(GetByIdAsync)))
            {
#if DEBUG
                Log.LogDebug("id = {id}", id);
#endif
                var entity = await Context.GetDbSet<TEntityModel>().FindAsync(id);
                if (entity is not null)
                {
                    Context.SetDetached(entity);
                }
                else
                {
#if DEBUG
                    Log.LogDebug("entity = {entity}", "NULL");
#endif
                }
#if DEBUG
                Log.LogDebug("entity = {entity}", entity);
#endif
                return entity;
            }
        }

        public async Task<IOperationResult<TEntity>> CreateAsync(TEntity entity)
        {
            _ = entity ?? throw new ArgumentNullException(nameof(entity));

            using (Log.BeginScope(nameof(CreateAsync)))
            {
#if DEBUG
                Log.LogDebug($"{nameof(entity)} = {entity}");
#endif
                if (entity is not IEntityBase)
                {
                    entity = Create(entity);
                }
                var id = ((IEntityBase)entity).GetId();

                var dbEntity = id == Guid.Empty
                    ? null
                    : await Context.GetDbSet<TEntityModel>().FindAsync(id);

                var wasOverriden = false;
                if (dbEntity is not null)
                {
                    Map(dbEntity, entity);
                    wasOverriden = true;
                }
                else
                {
                    dbEntity = Create(entity);
                    _ = await Context.GetDbSet<TEntityModel>().AddAsync(dbEntity);
                }

                var count = 1;
                if (AutoSaveChanges)
                {
                    count = await Context.SaveChangesAsync();
                }
#if DEBUG
                Log.LogDebug($"{nameof(entity)} = {entity}");
#endif
                return new OperationResult<TEntity>(dbEntity, wasOverriden, count) ;
            }
        }

        public async Task<IOperationResult<TEntity>> UpdateAsync(TEntity entity)
        {
            _ = entity ?? throw new ArgumentNullException(nameof(entity));

            using (Log.BeginScope(nameof(UpdateAsync)))
            {
#if DEBUG
                Log.LogDebug($"{nameof(entity)} = {entity}");
#endif
                if (entity is not IEntityBase)
                {
                    entity = Create(entity);
                }
                var id = ((IEntityBase)entity).GetId();

                var dbEntity = await Context.GetDbSet<TEntityModel>().FindAsync(id);

                if (dbEntity is null)
                {
#if DEBUG
                    Log.LogWarning($"Not found for update {nameof(id)} = {id} {nameof(entity)}={entity}");
#endif
                    return new OperationResult<TEntity>();
                }
                else
                {
#if DEBUG
                    Log.LogDebug($"{nameof(dbEntity)} = {dbEntity}");
#endif
                    Map(dbEntity, entity);
                    Context.SetModified(dbEntity);
                }
                OperationResult<TEntity> result;
                if (AutoSaveChanges)
                {
                    var count = await Context.SaveChangesAsync();
                    result = new OperationResult<TEntity>(dbEntity, wasOverriden: false, count);
                }
                else
                {
                    result = new OperationResult<TEntity>(dbEntity);
                }
#if DEBUG
                Log.LogDebug($"{nameof(result)} = {result}");
#endif
                return result;
            }
        }

        public async Task<IOperationResult<TEntity>> DeleteAsync(Id<TEntityId> id)
        {
            using (Log.BeginScope(nameof(DeleteAsync)))
            {
#if DEBUG
                Log.LogDebug($"{nameof(id)} = {id}");
#endif
                var dbEntity = await Context.GetDbSet<TEntityModel>().FindAsync(id);

                if (dbEntity is null)
                {
#if DEBUG
                    Log.LogWarning($"Not found for delete {nameof(id)} = {id}");
#endif
                    return new OperationResult<TEntity>();
                }
                else
                {
#if DEBUG
                    Log.LogDebug($"{nameof(dbEntity)} = {dbEntity}");
#endif
                    Context.GetDbSet<TEntityModel>().Remove(dbEntity);
                }
                OperationResult<TEntity> result;
                if (AutoSaveChanges)
                {
                    var count = await Context.SaveChangesAsync();
                    result = new OperationResult<TEntity>(dbEntity, wasOverriden: false, count);
                }
                else
                {
                    result = new OperationResult<TEntity>(dbEntity);
                }
#if DEBUG
                Log.LogDebug($"{nameof(result)} = {result}");
#endif
                return result;
            }
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing && _lazyContext.IsValueCreated)
                {
                    _lazyContext.Value.Dispose();
                }
            }
            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}

