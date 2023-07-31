namespace SushiDelivery.DAL.Interfaces
{
    /// <summary>
    /// The Unit of Work Pattern in C# is used to group one or more operations (usually database CRUD operations) into a single transaction and execute them by applying the principle of do everything or do nothing.
    /// Any changes made trough repositories are not applied to the database until <see>SaveChangesAsync</see> is called.
    /// <seealso cref="https://enlabsoftware.com/development/how-to-implement-repository-unit-of-work-design-patterns-in-dot-net-core-practical-examples-part-one.html"/>
    /// <seealso cref="https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application"/>
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }

        ICustomerRepository CustomerRepository { get; }

        Task<IOperationResult<Guid[]>> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
