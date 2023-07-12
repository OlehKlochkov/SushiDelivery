using SushiDelivery.DAL.Infrastructure;

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

        private readonly ISushiDeliveryContext _context;

        private IProductRepository _productRepository;

        private ICustomerRepository _customerRepository;

        #region Contructor

        public UnitOfWork(ISushiDeliveryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_context);
                }

                return _productRepository;
            }
        }

        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                {
                    _customerRepository = new CustomerRepository(_context);
                }

                return _customerRepository;
            }
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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
