using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPedido.Interfaces;

namespace XPedido.Services
{
    public class ProductService : IProductService
    {
        private readonly IApi _Api;
        public ProductService(IApi api)
        {
            _Api = api;
        }
        public async Task<Product> GetProductById(int Id)
        {
            List<Product> products = await GetProducts();
            return products.FirstOrDefault(x => x.Id == Id);
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _Api.GetProducts();
        }

        public async Task<List<Product>> GetProductsByCategoryId(int Id)
        {
            List<Product> products = await GetProducts();
            return products.Where(x => x.Category_id == Id).ToList();
        }
    }
}
