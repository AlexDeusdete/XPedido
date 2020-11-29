using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XPedido.Models
{
    public class Order
    {
        public int ID { get; set; }
        private readonly List<OrderProduct> _products = new List<OrderProduct>();

        public Order(int ID)
        {
            this.ID = ID;
        }

        public double GetTotalAfterDiscount() => _products.Sum(x => x.GetTotalPriceAfterDiscount());

        public int GetTotalQuantityProducts() => _products.Sum(x => x.Quantity);

        public OrderProduct GetOrderProduct(Product product) => _products.FirstOrDefault(x => x.Product.Id == product?.Id);

        public IReadOnlyCollection<OrderProduct> GetOrderProducts() => _products.AsReadOnly();

        public bool AddProduct(Product product, int Quantity, ProductPromotion productPromotion)
        {
            try
            {
                if (_products.Any(x => x.Product.Id == product.Id))
                {
                    //Pega o Produto que já existia e atualiza a quantidade dele
                    OrderProduct existingOrderProduct = _products.FirstOrDefault(x => x.Product.Id == product.Id);
                    existingOrderProduct.IncrementDecrementQuantity(Quantity - existingOrderProduct.Quantity);
                    return true;
                }

                _products.Add(new OrderProduct(product, Quantity, productPromotion));
                return true;
            }
            catch (ArgumentNullException)
            {
                return false;
            }

        }

        public bool RemoveProduct(OrderProduct orderProduct)
        {
            return _products.Remove(orderProduct);
        }

        public bool UpdateQuantityProduct(Product product, int quantity)
        {
            if (_products.Any(x => x.Product.Id == product.Id))
            {
                OrderProduct orderProduct = _products.FirstOrDefault(x => x.Product.Id == product.Id);
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
