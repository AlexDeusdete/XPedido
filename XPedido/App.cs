using MvvmCross;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using XPedido.Interfaces;
using XPedido.Services;
using XPedido.Core.ViewModels;
using MvvmCross.IoC;

namespace XPedido
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {            
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.IoCProvider.RegisterSingleton<IApi>(() => new Api());

            RegisterAppStart<CatalogViewModel>();
        }
    }
}
