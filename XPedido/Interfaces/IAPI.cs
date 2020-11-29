using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XPedido.Interfaces
{
    public interface IApi
    {
        #region Category
        [Get("/YNR2rsWe")]
        Task<List<Category>> GetCategories();
        #endregion

        #region Product
        [Get("/eVqp7pfX")]
        Task<List<Product>> GetProducts();
        #endregion

        #region ProductPromotion
        [Get("/R9cJFBtG")]
        Task<List<ProductPromotion>> GetProductPromotions();
        #endregion
    }
}
