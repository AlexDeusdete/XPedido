using System;
using System.Collections.Generic;
using System.Text;

namespace XPedido.Models
{
    public class Order
    {
        public int ID { get; set; }
        public IList<OrderProduct> Products { get; set; }
    }
}
