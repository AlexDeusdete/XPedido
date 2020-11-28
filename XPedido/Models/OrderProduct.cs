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
        private readonly ProductPromotion _productPromotion;

        public OrderProduct(Product product, int quantity, ProductPromotion productPromotion)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            SetQuantity(quantity);
            _productPromotion = productPromotion;
        }

        public void IncrementDecrementQuantity(int IncrementDecrement)
        {
            SetQuantity(Quantity + IncrementDecrement);
        }

        private void SetQuantity(int quantity)
        {
            Quantity = quantity < 0 ? 0 : quantity;
        }

        public bool HasDiscont() => _productPromotion?.Policies.Any(x => x.Min <= Quantity) ?? false;

        public double GetValueDiscount()
        {
            return ((GetPercentDiscount() / 100.0) * Product.Price) * Quantity;
        }

        public double GetPercentDiscount()
        {
            float Desc = HasDiscont() ? _productPromotion.Policies.Where(x => x.Min <= Quantity).Max(x => x.Discount) : 0;
            Desc = Desc < 0 ? 0 : Desc;
            return Desc > 100 ? 0 : Desc;
        }

        public double GetTotalPriceAfterDiscount()
        {
            return (Quantity * Product.Price) - GetValueDiscount();
        }

    }
}
