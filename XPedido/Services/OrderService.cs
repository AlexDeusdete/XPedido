using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XPedido.Interfaces;
using XPedido.Models;

namespace XPedido.Services
{
    public class OrderService : IOrderService
    {
        public async Task<Order> CreateOrder()
        {
            return new Order(1);
        }

        public async Task<bool> FinalizeOrder()
        {
            return true;
        }

        public Task<Order> GetOrderById()
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetOrders()
        {
            throw new NotImplementedException();
        }
    }
}
