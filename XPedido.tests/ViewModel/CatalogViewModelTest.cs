using Castle.Core.Internal;
using Moq;
using MvvmCross.Base;
using MvvmCross.Navigation;
using MvvmCross.Tests;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XPedido.Interfaces;
using XPedido.Models;
using XPedido.ViewModels;
using Xunit;

namespace XPedido.tests.ViewModel
{
    public class CatalogViewModelTest
    {
        readonly NavigationTestFixture _fixture;
        public CatalogViewModelTest()
        {
            _fixture = new NavigationTestFixture();
            _fixture.ClearAll();
            _fixture.Ioc.RegisterSingleton<IMvxMainThreadAsyncDispatcher>(new DummyDispatcher());
        }

        [Fact]
        public async void ProductsShouldBeEmpty()
        {
            //Arrange
            Moq.Mock<IProductService> productServiceMock = new Moq.Mock<IProductService>();
            productServiceMock.Setup(x => x.GetProducts())
                .Returns(Task.FromResult(new List<Product>()));

            Moq.Mock<IOrderService> orderServiceMock = new Moq.Mock<IOrderService>();
            orderServiceMock.Setup(x => x.CreateOrder())
                .Returns(Task.FromResult(new Order(1)));

            Moq.Mock<ICategoryService> categoryServiceMock = new Moq.Mock<ICategoryService>();
            Moq.Mock<IProductPromotionService> productPromotionServiceMock = new Moq.Mock<IProductPromotionService>();

            CatalogViewModel catalogViewModel = new CatalogViewModel(productServiceMock.Object, orderServiceMock.Object, categoryServiceMock.Object, productPromotionServiceMock.Object);

            //Act
            await catalogViewModel.Initialize();
            var actual = catalogViewModel.Products;

            //Assert
            Assert.Empty(actual);
        }

        [Fact]
        public async void ProductsNotShouldBeEmpty()
        {
            //Arrange
            Moq.Mock<IProductService> productServiceMock = new Moq.Mock<IProductService>();
            productServiceMock.Setup(x => x.GetProducts())
                .Returns(Task.FromResult(new List<Product>() { new Product() { Id = 1, Name = "Produto Teste" } }));

            Moq.Mock<IOrderService> orderServiceMock = new Moq.Mock<IOrderService>();
            orderServiceMock.Setup(x => x.CreateOrder())
                .Returns(Task.FromResult(new Order(1)));

            Moq.Mock<ICategoryService> categoryServiceMock = new Moq.Mock<ICategoryService>();
            Moq.Mock<IProductPromotionService> productPromotionServiceMock = new Moq.Mock<IProductPromotionService>();

            CatalogViewModel catalogViewModel = new CatalogViewModel(productServiceMock.Object, orderServiceMock.Object, categoryServiceMock.Object, productPromotionServiceMock.Object);



            //Act
            await catalogViewModel.Initialize();
            var actual = catalogViewModel.Products;

            //Assert
            Assert.NotEmpty(actual);
        }

        [Theory]
        [InlineData(2, 0, 2)]
        [InlineData(3, 2, 1)]
        [InlineData(1, 1, 0)]
        public async void ProductsQuantityShouldBeEqualsTheResult(int increment, int decrement, int result)
        {
            //Arrange
            Product product = new Product() { Id = 1, Name = "Produto Teste", Price = 100 };

            Moq.Mock<IProductService> productServiceMock = new Moq.Mock<IProductService>();
            productServiceMock.Setup(x => x.GetProducts())
                .Returns(Task.FromResult(new List<Product>() { product }));

            Moq.Mock<IOrderService> orderServiceMock = new Moq.Mock<IOrderService>();
            orderServiceMock.Setup(x => x.CreateOrder())
                .Returns(Task.FromResult(new Order(1)));

            Moq.Mock<ICategoryService> categoryServiceMock = new Moq.Mock<ICategoryService>();
            Moq.Mock<IProductPromotionService> productPromotionServiceMock = new Moq.Mock<IProductPromotionService>();

            CatalogViewModel catalogViewModel = new CatalogViewModel(productServiceMock.Object, orderServiceMock.Object, categoryServiceMock.Object, productPromotionServiceMock.Object);

            //Act
            await catalogViewModel.Initialize();

            for (int i = 0; i < increment; i++)
            {
                catalogViewModel.IncrementProductQuantityCommand.Execute(product);
            }

            for (int i = 0; i < decrement; i++)
            {
                catalogViewModel.DecrementProductQuantityCommand.Execute(product);
            }                      
            var actual = catalogViewModel.TotalItems;

            //Assert
            Assert.Equal(result, actual);
        }
    }
}
