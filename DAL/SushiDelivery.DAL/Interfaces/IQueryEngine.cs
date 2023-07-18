namespace SushiDelivery.DAL.Interfaces
{
    public interface IQueryEngine<TEntity> : IDisposable
        where TEntity : class
    {
        public IQueryable<TEntity> Items { get; }
    }
}
