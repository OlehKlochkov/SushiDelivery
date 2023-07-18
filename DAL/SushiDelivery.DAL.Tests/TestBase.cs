using Xunit.Abstractions;

namespace SushiDelivery.DAL.Tests.Infrastructure
{
    public class TestBase : IDisposable
    {
        public TestBase(ITestOutputHelper output)
        {
            Log = output;
            Log.WriteLine($"{GetType().FullName}: {"Constructor"}");
        }

        protected ITestOutputHelper Log { get; }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Log?.WriteLine($"{GetType().FullName}: {"Destructor"}");
            }
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}
