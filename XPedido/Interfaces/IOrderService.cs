using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XPedido.Models;

namespace XPedido.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrders();
        Task<Order> GetOrderById();
        Task<Order> CreateOrder();

        Task<bool> FinalizeOrder();
    }
}
