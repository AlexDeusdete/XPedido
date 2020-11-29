using MvvmCross;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using XPedido.Interfaces;
using XPedido.Services;
using XPedido.ViewModels;

namespace XPedido
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.RegisterSingleton<IApi>(() => new Api());
            Mvx.IoCProvider.RegisterType<ICategoryService, CategoryService>();
            Mvx.IoCProvider.RegisterType<IOrderService, OrderService>();
            Mvx.IoCProvider.RegisterType<IProductService, ProductService>();
            Mvx.IoCProvider.RegisterType<IProductPromotionService, ProductPromotionService>();

            RegisterAppStart<CatalogViewModel>();
        }
    }
}
