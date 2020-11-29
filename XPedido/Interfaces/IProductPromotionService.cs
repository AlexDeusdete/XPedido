using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XPedido.Interfaces
{
    public interface IProductPromotionService
    {
        Task<List<ProductPromotion>> GetProductPromotions();

        Task<ProductPromotion> GetProductPromotionByCategoryId(int Id);

    }
}
