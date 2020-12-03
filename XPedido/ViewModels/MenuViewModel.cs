using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XPedido.Core.ViewModels;

namespace XPedido.Core.ViewModels
{
    public class MenuViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public IMvxAsyncCommand ShowCatalogCommand { get; private set; }
        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowCatalogCommand = new MvxAsyncCommand(NavigateToCatalogAsync);
        }

        private Task NavigateToCatalogAsync()
        {
            return _navigationService.Navigate<CatalogViewModel>();
        }
    }
}
