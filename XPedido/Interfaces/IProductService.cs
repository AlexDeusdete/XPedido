using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XPedido.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task<List<Product>> GetProductsByCategoryId(int Id);
        Task<Product> GetProductById(int Id);
    }
}
