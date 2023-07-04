using Xunit.Abstractions;

namespace SushiDelivery.DAL.Tests.Infrastructure
{
    public class TestBase : IDisposable
    {
        private readonly ITestOutputHelper _output;

        public TestBase(ITestOutputHelper output)
        {
            this._output = output;
            this._output.WriteLine("{0}: {1}", this.GetType().FullName, "Constructor");
        }

        protected ITestOutputHelper Log => _output;

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_output != null)
                {
                    this._output.WriteLine("{0}: {1}", this.GetType().FullName, "Destructor");
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}
