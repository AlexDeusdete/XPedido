using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XPedido.Models;
using Xunit;

namespace XPedido.tests.Models
{
    public class OrderTest
    {
        [Fact]
        public void ProductsShouldBeEmpty()
        {
            //Arrange
            Order order = new Order(1);
            var prod = new Product() { Id = 1 };
            order.AddProduct(prod, 1, null);
            order.RemoveProduct(order.GetOrderProduct(prod));

            //Act
            IReadOnlyCollection<OrderProduct> actual = order.GetOrderProducts();

            //Assert
            Assert.Empty(actual);
        }

        [Fact]
        public void ProductsNotShouldBeEmpty()
        {
            //Arrange
            Order order = new Order(1);
            order.AddProduct(new Product(), 1, null);

            //Act
            IReadOnlyCollection<OrderProduct> actual = order.GetOrderProducts();

            //Assert
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void GetTotalAfterDiscountShouldBeCorrectly()
        {
            //Arrange
            Order order = new Order(1);

            Product product = new Product() { Id = 1, Category_id = 1, Description = "Teste Descrição", Name = "teste", Price = 850 };
            order.AddProduct(product, 1, null);

            product = new Product() { Id = 2, Category_id = 1, Description = "Teste Descrição", Name = "teste", Price = 350 };
            order.AddProduct(product, 3, null);

            //Act
            double actual = order.GetTotalAfterDiscount();

            //Assert
            Assert.Equal(1900, actual);
        }

        [Fact]
        public void GetTotalQuantityProductsShouldBeCorrectly()
        {
            //Arrange
            Order order = new Order(1);

            Product product = new Product() { Id = 1, Category_id = 1, Description = "Teste Descrição", Name = "teste", Price = 850 };            
            order.AddProduct(product, 1, null);

            product = new Product() { Id = 2, Category_id = 1, Description = "Teste Descrição", Name = "teste", Price = 350 };
            order.AddProduct(product, 3, null);

            //Act
            int actual = order.GetTotalQuantityProducts();

            //Assert
            Assert.Equal(4, actual);
        }

        [Fact]
        public void GetOrderProductShouldBeNull()
        {            
            //Arrange
            Order order = new Order(1);            

            Product product = new Product() { Id = 1, Category_id = 1, Description = "Teste Descrição", Name = "teste", Price = 850 };

            //Act
            OrderProduct actual = order.GetOrderProduct(product);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void GetOrderProductNotShouldBeNull()
        {
            //Arrange
            Order order = new Order(1);
            Product product = new Product() { Id = 1, Category_id = 1, Description = "Teste Descrição", Name = "teste", Price = 850 };
            order.AddProduct(product, 1, null);

            //Act
            OrderProduct actual = order.GetOrderProduct(product);

            //Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public void GetTotalAfterDiscountShouldBeZero()
        {
            //Arrange
            Order order = new Order(1);

            //Act
            double actual = order.GetTotalAfterDiscount();

            //Assert
            Assert.Equal(0, actual);
        }

        [Theory]
        [InlineData(4, 4)]
        [InlineData(2, 2)]
        [InlineData(-3, 0)]
        public void UpdateQuantityProduct_QuantityShouldBeEqualsTheResult(int newQuantity, int result)
        {
            //Arrange
            Order order = new Order(1);

            Product product = new Product() { Id = 1, Category_id = 1, Description = "Teste Descrição", Name = "teste", Price = 850 };
            order.AddProduct(product, 1, null);

            //Act
            order.UpdateQuantityProduct(product, newQuantity);
            int actual = order.GetOrderProduct(product)?.Quantity ?? 0;

            //Assert
            Assert.Equal(result, actual);
        }
    }
}

