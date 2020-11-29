using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XPedido.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
    }
}
