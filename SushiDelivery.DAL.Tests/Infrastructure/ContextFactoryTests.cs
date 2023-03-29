using SushiDelivery.DAL.Infrastructure;
using System.ComponentModel;

namespace SushiDelivery.DAL.Tests.Infrastructure
{
    public class ContextFactoryTests
    {

        private ContextFactory _contextFactory;

        public ContextFactoryTests()
        {
            _contextFactory = new ContextFactory();
        }

        [Fact]
        [Description("Test verifies that factory creates instance of ISushiDeliveryContext.")]
        public void TestCreate_ISushiDeliveryContext()
        {
            //Act
            ISushiDeliveryContext context = _contextFactory.CreateDbContext();
            
            //Assert
            Assert.NotNull(context);
        }
    }
}