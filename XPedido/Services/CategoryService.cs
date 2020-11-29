using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPedido.Interfaces;

namespace XPedido.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IApi _Api;
        public CategoryService(IApi api)
        {
            _Api = api;
        }
        public async Task<List<Category>> GetCategories()
        {
            return await _Api.GetCategories();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            List<Category> Categories = await GetCategories();

            return Categories.FirstOrDefault(x => x.Id == id);
        }
    }
}
