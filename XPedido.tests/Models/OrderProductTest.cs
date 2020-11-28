﻿using System;
using System.Collections.Generic;
using System.Text;
using XPedido.Models;
using Xunit;

namespace XPedido.tests.Models
{
   
    public class OrderProductTest
    {
        [Theory]
        [InlineData(1, -2)]
        [InlineData(0, -300)]
        [InlineData(1, -1)]
        public void QuantityNotShoudBeNegative(int inicio, int incrementDecrement)
        {
            //Arrange
            Moq.Mock<Product> mock = new Moq.Mock<Product>();

            OrderProduct orderProduct = new OrderProduct(mock.Object, inicio);

            //Act
            orderProduct.IncrementDecrementQuantity(incrementDecrement);

            //Assert
            Assert.False(orderProduct.Quantity < 0);
        }

        [Theory]
        [InlineData(-2, 1, 1)]
        [InlineData(0, 7, 7)]
        [InlineData(1, 15, 16)]
        public void QuantityShoudBeIncremented(int inicio, int incrementDecrement, int result)
        {
            //Arrange
            Moq.Mock<Product> mock = new Moq.Mock<Product>();

            OrderProduct orderProduct = new OrderProduct(mock.Object, inicio);

            //Act
            orderProduct.IncrementDecrementQuantity(incrementDecrement);

            //Assert
            Assert.True(orderProduct.Quantity == result);
        }

        [Theory]
        [InlineData(6, -1, 5)]
        [InlineData(4, -2, 2)]
        [InlineData(1, -15, 0)]
        public void QuantityShoudBeDecremented(int inicio, int incrementDecrement, int result)
        {
            //Arrange
            Moq.Mock<Product> mock = new Moq.Mock<Product>();

            OrderProduct orderProduct = new OrderProduct(mock.Object, inicio);

            //Act
            orderProduct.IncrementDecrementQuantity(incrementDecrement);

            //Assert
            Assert.True(orderProduct.Quantity == result);
        }

        [Fact]
        public void ProductNotShoudBeNull()
        { 
            //Assert
            Assert.Throws<ArgumentNullException>(() => new OrderProduct(null, 1));
        }
    }
}
