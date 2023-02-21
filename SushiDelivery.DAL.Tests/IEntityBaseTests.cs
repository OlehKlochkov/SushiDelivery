using SushiDelivery.DAL.Models;

namespace SushiDelivery.Domain.Tests
{
    public class IEntityBaseTests
    {
        [Fact]
        public void EntityBaseTest()
        {
            var customer = new Customer() { LoginName = "TEST"};
            var entity = customer as IEntityBase;
        }
    }
}
