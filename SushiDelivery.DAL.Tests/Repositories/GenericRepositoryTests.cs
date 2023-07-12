using Microsoft.EntityFrameworkCore;
using Moq;
using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.DAL.Repositories;

namespace SushiDelivery.DAL.Tests.Repositories
{
    internal static class GenericRepositoryTests
    {
        public static async Task TestSaveAsync<TEntity, TEntityId>(Mock<ISushiDeliveryContext> contextMock,
            GenericRepository<TEntity, TEntityId> repository, TEntity entity)
            where TEntity : class
            where TEntityId : struct
        {
            // Arrange
            var entities = new List<TEntity>();
            var mockSet = new Mock<DbSet<TEntity>>();

            // Act
            mockSet.Setup(m => m.AddAsync(It.IsAny<TEntity>(), It.IsAny<CancellationToken>()))
            .Callback((TEntity e, CancellationToken c) => entities.Add(e));

            contextMock.Setup(m => m.GetDbSet<TEntity>()).Returns(mockSet.Object);

            await repository.SaveAsync(entity);

            // Assert
            Assert.NotNull(entities);
            Assert.Equal(entity, entities[0]);

            mockSet.Verify(m => m.AddAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
            contextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }


        public static async Task TestIncludeAsync<TEntity, TEntityId>(Mock<ISushiDeliveryContext> contextMock,
            GenericRepository<TEntity, TEntityId> repository, TEntity entity)
            where TEntity : class
            where TEntityId : struct
        {
            // Arrange
            var entities = new List<TEntity>();
            var mockSet = new Mock<DbSet<TEntity>>();

            // Act
            mockSet.Setup(m => m.Attach(It.IsAny<TEntity>()))
             .Callback((TEntity e) => entities.Add(e));

            contextMock.Setup(m => m.GetDbSet<TEntity>()).Returns(mockSet.Object);

            await repository.UpdateAsync(entity);

            // Assert
            Assert.NotNull(entities);
            Assert.Equal(entity, entities[0]);

            mockSet.Verify(m => m.Attach(entity), Times.Once);
            contextMock.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        public static async Task TestGetByIdAsync<TEntity, TEntityId>(Mock<ISushiDeliveryContext> contextMock,
            GenericRepository<TEntity, TEntityId> repository, TEntity entity, TEntityId id)
            where TEntity : class
            where TEntityId : struct
        {
            // Arrange
            var mockSet = new Mock<DbSet<TEntity>>();

            // Act
            mockSet.Setup(m => m.FindAsync(It.IsAny<TEntityId>())).ReturnsAsync(entity);
            contextMock.Setup(m => m.GetDbSet<TEntity>()).Returns(mockSet.Object);

            var receivedEntity = await repository.GetByIdAsync(id);

            // Assert
            Assert.NotNull(receivedEntity);
            Assert.Equal(entity, receivedEntity);
            mockSet.Verify(x => x.FindAsync(It.IsAny<TEntityId>()), Times.Once);
            contextMock.Verify(m => m.SetDetached(receivedEntity), Times.Once);
        }
    }
}
