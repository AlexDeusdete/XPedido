using System;
using System.Collections.Generic;
using System.Text;
using XPedido.Models;
using Xunit;

namespace XPedido.tests.Models
{
   
    public class OrderProductTest
    {
        [Fact]
        public void QuantityNotShoudBeNegative()
        {
            //Arrange
            Moq.Mock<Product> mock = new Moq.Mock<Product>();

            OrderProduct orderProduct = new OrderProduct(mock.Object, 1);

            //Act
            orderProduct.IncrementDecrementQuantity(-2);

            //Assert
            Assert.True(orderProduct.Quantity >= 0);
        }
    }
}
