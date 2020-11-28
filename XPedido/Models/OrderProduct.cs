using System;
using System.Collections.Generic;
using System.Text;

namespace XPedido.Models
{
    public class OrderProduct
    {
        public int ID { get; set; }
        public int Quantity { get; private set; }
        public Product Product { get; private set; }

        public OrderProduct(Product product, int quantity)
        {
            Product = product;
            SetQuantity(quantity);
        }

        public void IncrementDecrementQuantity(int IncrementDecrement )
        {
            SetQuantity(Quantity + IncrementDecrement);
        }

        private void SetQuantity(int quantity)
        {
            Quantity = quantity < 0 ? 0 : quantity;
        }
    }
}
