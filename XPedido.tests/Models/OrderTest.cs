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
            order.AddProduct(new Product(), 1, null);
            order.RemoveProduct(order.Products[0]);

            //Act
            IList<OrderProduct> actual = order.Products;

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
            IList<OrderProduct> actual = order.Products;

            //Assert
            Assert.Empty(actual);
        }

        [Fact]
        public void GetTotalAfterDiscountShouldBeCorrectly()
        {
            //Arrange
            Order order = new Order(1);

            Product product = new Product() { Id = 1, Category_id = 1, Description = "Teste Descrição", Name = "teste", Price = 850 };
            OrderProduct orderProduct = new OrderProduct(product, 1, null);
            order.Products.Add(orderProduct);

            product = new Product() { Id = 2, Category_id = 1, Description = "Teste Descrição", Name = "teste", Price = 350 };
            orderProduct = new OrderProduct(product, 3, null);
            order.Products.Add(orderProduct);

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
            OrderProduct orderProduct = new OrderProduct(product, 1, null);
            order.Products.Add(orderProduct);

            product = new Product() { Id = 2, Category_id = 1, Description = "Teste Descrição", Name = "teste", Price = 350 };
            orderProduct = new OrderProduct(product, 3, null);
            order.Products.Add(orderProduct);

            //Act
            int actual = order.GetTotalQuantityProducts();

            //Assert
            Assert.Equal(4, actual);
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
            OrderProduct orderProduct = new OrderProduct(product, 1, null);
            order.Products.Add(orderProduct);

            //Act
            order.UpdateQuantityProduct(product, newQuantity);
            int actual = order.Products.FirstOrDefault(x => x.Product.Id == product.Id)?.Quantity ?? 0;

            //Assert
            Assert.Equal(result, actual);
        }
    }
}

