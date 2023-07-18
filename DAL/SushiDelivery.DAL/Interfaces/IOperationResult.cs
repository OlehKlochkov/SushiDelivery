namespace SushiDelivery.DAL.Interfaces
{
    public interface IOperationResult<TEntity>
        where TEntity : class
    {
        public long Count { get; }

        public bool WasOverriden { get; }

        public TEntity? Entity { get; }
    }
}
