using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XPedido.Interfaces;

namespace XPedido.Services
{
    public class Api : IApi
    {
        private readonly IApi _api;
        public Api()
        {
            _api = RestService.For<IApi>("https://pastebin.com/raw");
        }
        public async Task<List<Category>> GetCategories()
        {
            return await _api.GetCategories();
        }

        public async Task<List<ProductPromotion>> GetProductPromotions()
        {
            return await _api.GetProductPromotions();
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _api.GetProducts();
        }
    }
}
