namespace SushiDelivery.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }

        ICustomerRepository CustomerRepository { get; }

        Task<IOperationResult<Guid[]>> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
