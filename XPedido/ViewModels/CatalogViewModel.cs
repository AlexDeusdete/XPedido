﻿using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using XPedido.Interfaces;
using XPedido.Models;

namespace XPedido.ViewModels
{
    public class CatalogViewModel : MvxViewModel
    {
        #region Services and Private Fiels
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly ICategoryService _categoryService;
        private readonly IProductPromotionService _productPromotionService;
        private Order _order;
        #endregion

        #region Ctor and Initialize
        public CatalogViewModel(IProductService productService,
                                IOrderService orderService,
                                ICategoryService categoryService,
                                IProductPromotionService productPromotionService)
        {
            _productService = productService;
            _orderService = orderService;
            _categoryService = categoryService;
            _productPromotionService = productPromotionService;

            Products = new MvxObservableCollection<Product>();
            Categories = new MvxObservableCollection<Category>();
        }

        public override async Task Initialize()
        {
            await base.Initialize();
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
        #endregion
    }
}
