using System;
using System.Collections.Generic;
using System.Text;
using XPedido.Models;
using Xunit;

namespace XPedido.tests.Models
{
   
    public class OrderProductTest
    {
        private OrderProduct GetOrderProductFake(int qtyProduct, float priceProduct = 0, int qtyMinPromotion = 0, float percentDiscount = 0)
        {
            Product product = new Product() { Category_id = 1, Description = "Descrição do Produto", Id = 1, Name = "Teste Produto", Photo = "", Price = priceProduct };
            ProductPromotion productPromotion = new ProductPromotion()
            {
                Category_id = 1,
                Name = "Promoção Teste",
                Policies = new Policy[1]
                {
                    new Policy() {Discount = percentDiscount, Min = qtyMinPromotion }
                }
            };
            return new OrderProduct(product, qtyProduct, productPromotion);
        }

        [Theory]
        [InlineData(1, -2)]
        [InlineData(0, -300)]
        [InlineData(1, -1)]
        public void QuantityNotShouldBeNegative(int inicio, int incrementDecrement)
        {
            //Arrange
            var orderProduct = GetOrderProductFake(inicio); ;

            //Act
            orderProduct.IncrementDecrementQuantity(incrementDecrement);

            //Assert
            Assert.False(orderProduct.Quantity < 0);
        }

        [Theory]
        [InlineData(-2, 1, 1)]
        [InlineData(0, 7, 7)]
        [InlineData(1, 15, 16)]
        public void QuantityShouldBeIncremented(int inicio, int incrementDecrement, int result)
        {
            //Arrange
            var orderProduct = GetOrderProductFake(inicio);

            //Act
            orderProduct.IncrementDecrementQuantity(incrementDecrement);

            //Assert
            Assert.True(orderProduct.Quantity == result);
        }

        [Theory]
        [InlineData(6, -1, 5)]
        [InlineData(4, -2, 2)]
        [InlineData(1, -15, 0)]
        public void QuantityShouldBeDecremented(int inicio, int incrementDecrement, int result)
        {
            //Arrange
            var orderProduct = GetOrderProductFake(inicio); 

            //Act
            orderProduct.IncrementDecrementQuantity(incrementDecrement);

            //Assert
            Assert.True(orderProduct.Quantity == result);
        }

        [Fact]
        public void ProductNotShouldBeNull()
        {
            //Arrange
            Product product = null;

            //Act
            Action action = () => new OrderProduct(product, 1, null);

            //Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Theory]
        [InlineData(6, 20.50, 5, 15)]
        [InlineData(4, 1000, 2, 10)]
        public void HasDiscountShouldBeTrue(int qtyProduct, float priceProduct, int qtyMinPromotion, float percentDiscount)
        {
            //Arrange
            var orderProduct = GetOrderProductFake(qtyProduct, priceProduct, qtyMinPromotion, percentDiscount);

            //Act
            var actual = orderProduct.HasDiscont();

            //Assert
            Assert.True(actual);
        }

        [Theory]
        [InlineData(6, 20.50, 7, 15)]
        [InlineData(4, 1000, 8, 10)]
        public void HasDiscountNotShouldBeTrue(int qtyProduct, float priceProduct, int qtyMinPromotion, float percentDiscount)
        {
            //Arrange
            var orderProduct = GetOrderProductFake(qtyProduct, priceProduct, qtyMinPromotion, percentDiscount);

            //Act
            var actual = orderProduct.HasDiscont();

            //Assert
            Assert.False(actual);
        }

        [Theory]
        [InlineData(6, 20.50, 5, 15, 18.45)]
        [InlineData(4, 1000, 2, 10, 400)]
        [InlineData(1, 2500, 2, 25, 0)]
        public void GetValueDiscountShouldBeEqualsTheResult(int qtyProduct, float priceProduct, int qtyMinPromotion, float percentDiscount, double result)
        {
            //Arrange
            var orderProduct = GetOrderProductFake(qtyProduct, priceProduct, qtyMinPromotion, percentDiscount);

            //Act
            double actual = orderProduct.GetValueDiscount();

            //Assert
            Assert.Equal(result, actual);
        }

        [Theory]
        [InlineData(6, 20.50, 5, 150)]
        [InlineData(4, 1000, 2, -30)]
        [InlineData(1, 2500, 2, -50)]
        public void GetValueDiscountNotShouldBeNegative(int qtyProduct, float priceProduct, int qtyMinPromotion, float percentDiscount)
        {
            //Arrange
            var orderProduct = GetOrderProductFake(qtyProduct, priceProduct, qtyMinPromotion, percentDiscount);

            //Act
            double actual = orderProduct.GetValueDiscount();

            //Assert
            Assert.False(actual < 0);
        }

        [Theory]
        [InlineData(6, 20.50, 5, 150)]
        [InlineData(4, 1000, 2, 550)]
        [InlineData(1, 2500, 2, -150)]
        public void GetValueDiscountNotShouldBeMoreThanTotalPrice(int qtyProduct, float priceProduct, int qtyMinPromotion, float percentDiscount)  
        {
            //Arrange
            var orderProduct = GetOrderProductFake(qtyProduct, priceProduct, qtyMinPromotion, percentDiscount);

            //Act
            double actual = orderProduct.GetValueDiscount();

            //Assert
            Assert.False(actual > 0);
        }
    }
}
    