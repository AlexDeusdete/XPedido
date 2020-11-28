using System;
using System.Collections.Generic;
using System.Linq;
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
            Product = product ?? throw new ArgumentNullException(nameof(product));
            SetQuantity(quantity);
        }
        public void IncrementDecrementQuantity(int IncrementDecrement)
        {
            SetQuantity(Quantity + IncrementDecrement);
        }
        private void SetQuantity(int quantity)
        {
            Quantity = quantity < 0 ? 0 : quantity;
        }

        public bool HasDiscont() => Product.Category.Promotion.Policies.Any(x => x.Min <= Quantity);
        public double GetValueDiscount()
        {
            float Desc = Product.Category.Promotion.Policies.Where(x => x.Min <= Quantity).Max(x => x.Discount);
            return (Desc / 100.0) * Product.Price;
        }

        public double GetValuePercent()
        {
            float Desc = Product.Category.Promotion.Policies.Where(x => x.Min <= Quantity).Max(x => x.Discount);
            return (Desc / 100.0) * Product.Price;
        }

    }
}
