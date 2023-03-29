namespace SushiDelivery.DAL.Repositories
{
    interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }

        ICustomerRepository CustomerRepository { get; }

        Task SaveChanges();
    }
}
