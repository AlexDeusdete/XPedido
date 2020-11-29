using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPedido.Interfaces;

namespace XPedido.Services
{
    public class ProductPromotionService : IProductPromotionService
    {
        private readonly IApi _Api;
        public ProductPromotionService(IApi api)
        {
            _Api = api;
        }
        public async Task<ProductPromotion> GetProductPromotionByCategoryId(int Id)
        {
            List<ProductPromotion> productPromotions = await GetProductPromotions();

            return productPromotions.FirstOrDefault(x => x.Category_id == Id);
        }

        public async Task<List<ProductPromotion>> GetProductPromotions()
        {
            return await _Api.GetProductPromotions();
        }
    }
}
