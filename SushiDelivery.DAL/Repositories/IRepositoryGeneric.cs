namespace SushiDelivery.DAL.Repositories
{
    internal interface IRepositoryGeneric<TEntity, TEntityId>
        where TEntity : class
        where TEntityId : struct
    {
        public IQueryable<TEntity> Query();

        public Task<TEntity?> GetById(TEntityId id);

        public Task<TEntity> Insert(TEntity entity);

        public Task<TEntity?> Delete(TEntityId id);

        public Task<TEntity?> Update(TEntity entityToUpdate);
    }
}
