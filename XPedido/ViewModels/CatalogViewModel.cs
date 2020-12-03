using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using XPedido.Interfaces;
using XPedido.Models;

namespace XPedido.Core.ViewModels
{
    public class CatalogViewModel : MvxViewModel
    {
        #region Services
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly ICategoryService _categoryService;
        private readonly IProductPromotionService _productPromotionService;
        private readonly IMvxNavigationService _navigationService;
        #endregion

        #region Ctor and Initialize
        public CatalogViewModel(IProductService productService,
                                IOrderService orderService,
                                ICategoryService categoryService,
                                IProductPromotionService productPromotionService,
                                IMvxNavigationService navigationService)
        {
            _productService = productService;
            _orderService = orderService;
            _categoryService = categoryService;
            _productPromotionService = productPromotionService;
            _navigationService = navigationService;

            Products = new MvxObservableCollection<Product>();
            Categories = new MvxObservableCollection<Category>();
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            await PopulateCategories();
            await PopulateProducts();

            _total = 0;
        }
        #endregion

        #region ObservableCollections
        public MvxObservableCollection<Product> Products;

        public MvxObservableCollection<Category> Categories;
        #endregion

        #region Private Methods
        private async Task PopulateProducts()
        {
            try
            {
                List<Product> products = await _productService.GetProducts();
                Products.Clear();
                Products.AddRange(products);
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
            }
        }

        private async Task PopulateProducts(int idCategory)
        {
            try
            {
                List<Product> products = await _productService.GetProductsByCategoryId(idCategory);
                Products.Clear();
                Products.AddRange(products);
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
            }
        }

        private async Task PopulateCategories()
        {
            try
            {
                List<Category> categories = await _categoryService.GetCategories();
                Categories.Clear();
                Categories.AddRange(categories);
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
            }
        }

        private void RecalculateTotals()
        {
            Total = _order.GetTotalAfterDiscount();
            TotalItems = _order.GetTotalQuantityProducts();
        }
        #endregion

        #region Propertys        
        private double _total;
        public double Total 
        {
            get => _total;
            set
            {
                _total = value;
                RaisePropertyChanged(() => Total);
            }
        }

        private int _totalItems;
        public int TotalItems
        {
            get => _totalItems;
            set
            {
                _totalItems = value;
                RaisePropertyChanged(() => TotalItems);
            }
        }

        private Order _order;
        private Order Order
        {
            get => _order;
            set
            {
                _order = value;
            }
        }
        #endregion

        #region Commands        
        private IMvxAsyncCommand<Product> _incrementProductQuantityCommand;
        public IMvxAsyncCommand<Product> IncrementProductQuantityCommand
        {
            get
            {
                _incrementProductQuantityCommand = _incrementProductQuantityCommand ?? new MvxAsyncCommand<Product>(async(product) => await IncrementProductQuantity(product));
                return _incrementProductQuantityCommand;
            }
        }
        private async Task IncrementProductQuantity(Product product)
        {
            if (_order == null)
                _order = await _orderService.CreateOrder();

            if (_order.GetOrderProduct(product) != null)
                _order.GetOrderProduct(product).IncrementDecrementQuantity(1);
            else
                _order.AddProduct(product, 1, await _productPromotionService.GetProductPromotionByCategoryId(product.Category_id ?? 0));

            RecalculateTotals();
        }

        private IMvxCommand _decrementProductQuantityCommand;
        public IMvxCommand DecrementProductQuantityCommand
        {
            get
            {
                _decrementProductQuantityCommand = _decrementProductQuantityCommand ?? new MvxCommand<Product>((product) => DecrementProductQuantity(product));
                return _decrementProductQuantityCommand;
            }
        }
        private void DecrementProductQuantity(Product product)
        {
            if (_order.GetOrderProduct(product) != null)
                _order.GetOrderProduct(product).IncrementDecrementQuantity(-1);

            RecalculateTotals();
        }

        private IMvxAsyncCommand<Category> _filterProductsByCategoryCommand;
        public IMvxAsyncCommand<Category> FilterProductsByCategoryCommand
        {
            get
            {
                _filterProductsByCategoryCommand = _filterProductsByCategoryCommand ?? new MvxAsyncCommand<Category>(async (category) => await FilterProductsByCategory(category));
                return _filterProductsByCategoryCommand;
            }
        }
        private async Task FilterProductsByCategory(Category category)
        {
            await PopulateProducts(category.Id);
        }

        private IMvxAsyncCommand _showMenuCommand;
        public IMvxAsyncCommand ShowMenuCommand
        {
            get
            {
                _showMenuCommand = _showMenuCommand ?? new MvxAsyncCommand(NavigateToMenuAsync);
                return _showMenuCommand;
            }
        }
        private Task NavigateToMenuAsync()
        {
            return _navigationService.Navigate<MenuViewModel>();
        }
        #endregion
    }
}
