using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.DAL.Interfaces;
using SushiDelivery.DAL.Models;

namespace SushiDelivery.DAL.Repositories
{
    /// <summary>
    /// Unit of work class shares a single database context.
    /// </summary>
    internal class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Flag that indicates whether object was disposed. 
        /// </summary>
        private bool _isDisposed;

        protected ILogger Log { get; }

        private readonly Lazy<ISushiDeliveryContext> _lazyContext;

        private IProductRepository? _productRepository;

        private ICustomerRepository? _customerRepository;

        #region Contructor

        public UnitOfWork(Lazy<ISushiDeliveryContext> lazyContext, ILogger logger)
        {
            _lazyContext = lazyContext ?? throw new ArgumentNullException(nameof(lazyContext));
            Log = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        public IProductRepository ProductRepository
        {
            get
            {
                _productRepository ??= new ProductRepository(_lazyContext, Log, false);
                return _productRepository;
            }
        }

        public ICustomerRepository CustomerRepository
        {
            get
            {
                _customerRepository ??= new CustomerRepository(_lazyContext, Log, false);
                return _customerRepository;
            }
        }

        public async Task<IOperationResult<Guid[]>> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            using (Log.BeginScope(nameof(SaveChangesAsync)))
            {
                if (!_lazyContext.IsValueCreated)
                {
                    return new OperationResult<Guid[]>();
                }

                var count = 0;
                var saveAttempts = 3;
                var ids = new List<Guid>();
                do
                {
                    try
                    {
                        count = await _lazyContext.Value.SaveChangesAsync(cancellationToken);
                        return new OperationResult<Guid[]>(count);
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        Log.LogError(ex,"{Message}", ex.Message);
                        saveAttempts--;

                        // Update original values from the database
                        foreach (var entity in ex.Entries)
                        {
                            var propertyValues = await entity.GetDatabaseValuesAsync(cancellationToken);
                            if (propertyValues != null)
                            {
                                ids.Add(((IEntityBase)entity.Entity).GetId());
                                entity.OriginalValues.SetValues(propertyValues);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.LogError(ex, "{Message}", ex.Message);
                        saveAttempts--;
                    }
                } while (saveAttempts > 0);
                return new OperationResult<Guid[]>(ids.ToArray(), ids.Count > 0, count);
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
