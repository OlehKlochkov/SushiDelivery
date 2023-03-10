using SushiDelivery.Domain.Models;

namespace SushiDelivery.Domain.Tests
{
    public class IdTests
    {
        [Fact]
        public void TestId()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var productId = new Id<IProductId>(guid);
            var customerId = new Id<ICustomerId>(guid);

            //Assert
            Assert.Equal(guid, (Guid)productId);
            Assert.Equal((Id<IProductId>)guid, productId);
            Assert.Equal((Guid)productId, (Guid)customerId);
            Assert.False(productId.Equals(customerId));
            Assert.Equal(productId.GetHashCode(), customerId.GetHashCode());
            Assert.True(productId != new Id<IProductId>(guid));
        }
    }
}
