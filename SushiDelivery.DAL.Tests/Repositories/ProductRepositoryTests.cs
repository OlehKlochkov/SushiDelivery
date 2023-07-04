using Moq;
using SushiDelivery.DAL.Infrastructure;
using SushiDelivery.DAL.Models;
using SushiDelivery.DAL.Repositories;
using SushiDelivery.Domain.Enumerations;
using SushiDelivery.Domain.Models;
using System.ComponentModel;

namespace SushiDelivery.DAL.Tests.Repositories
{
    public class ProductRepositoryTests
    {
        private static Models.Product _expectedProduct = new Models.Product
        {
            Id = new Id<IProductId>(Guid.NewGuid()),
            Name = "TestName",
            Price = (decimal)0.99,
            IsAvailable = true,
            Category = Categories.Dessert,
            ProductIngredients = new List<ProductIngredient>()
        };

        private Mock<ISushiDeliveryContext> _contextMock;
        private ProductRepository _productRepository;

        public ProductRepositoryTests()
        {
            _contextMock = new Mock<ISushiDeliveryContext>();
            _productRepository = new ProductRepository(_contextMock.Object);
        }

        [Fact]
        [Description("Test verifies get by id entity from database.")]
        public void TestGetByIdAsync()
        {
            //TODO:
        }

        [Fact]
        [Description("Test verifies save data into the database.")]
        public async void TestSaveProduct()
        {
            await GenericRepositoryTests.TestSaveAsync(_contextMock, _productRepository, _expectedProduct);
        }


        [Fact]
        [Description("Test verifies include data into the database.")]
        public async void TestIncludeProduct()
        {
            await GenericRepositoryTests.TestIncludeAsync(_contextMock, _productRepository, _expectedProduct);
        }
    }



}
