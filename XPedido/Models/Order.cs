using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XPedido.Models
{
    public class Order
    {
        public int ID { get; set; }
        public readonly IList<OrderProduct> Products = new List<OrderProduct>();

        public Order(int ID)
        {
            this.ID = ID;
        }

        public double GetTotalAfterDiscount() => Products.Sum(x => x.GetTotalPriceAfterDiscount());

        public int GetTotalQuantityProducts() => Products.Sum(x => x.Quantity);

        public bool AddProduct(Product product, int Quantity, ProductPromotion productPromotion)
        {
            try
            {
                if (Products.Any(x => x.Product.Id == product.Id))
                {
                    //Pega o Produto que já existia e atualiza a quantidade dele
                    OrderProduct existingOrderProduct = Products.FirstOrDefault(x => x.Product.Id == product.Id);
                    existingOrderProduct.IncrementDecrementQuantity(Quantity - existingOrderProduct.Quantity);
                    return true;
                }

                Products.Add(new OrderProduct(product, Quantity, productPromotion));
                return true;
            }
            catch (ArgumentNullException)
            {
                return false;
            }

        }

        public bool RemoveProduct(OrderProduct orderProduct)
        {
            return Products.Remove(orderProduct);
        }

        public bool UpdateQuantityProduct(Product product, int quantity)
        {
            if (Products.Any(x => x.Product.Id == product.Id))
            {
                OrderProduct orderProduct = Products.FirstOrDefault(x => x.Product.Id == product.Id);
                orderProduct.IncrementDecrementQuantity(quantity - orderProduct.Quantity);

                if (orderProduct.Quantity <= 0)
                    RemoveProduct(orderProduct);

                return true;
            }
            else
                return false;
        }
    }
}
