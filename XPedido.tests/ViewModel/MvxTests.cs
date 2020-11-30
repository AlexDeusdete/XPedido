using Moq;
using MvvmCross.Base;
using MvvmCross.Navigation;
using MvvmCross.Tests;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XPedido.tests.ViewModel
{
    public class DummyDispatcher : MvxSingleton<IMvxMainThreadAsyncDispatcher>, IMvxMainThreadAsyncDispatcher
    {
        public bool IsOnMainThread => true;

        public Task ExecuteOnMainThreadAsync(Action action, bool maskExceptions = true)
        {
            action?.Invoke();
            return Task.CompletedTask;
        }

        public Task ExecuteOnMainThreadAsync(Func<Task> action, bool maskExceptions = true)
        {
            return action?.Invoke();
        }
    }

    public class NavigationTestFixture : MvxTestFixture
    {
        protected override void AdditionalSetup()
        {
            var navigationServiceMock = new Mock<IMvxNavigationService>();
            Ioc.RegisterSingleton(navigationServiceMock.Object);
            Ioc.RegisterSingleton(new MvxDefaultViewModelLocator());
        }
    }
}
