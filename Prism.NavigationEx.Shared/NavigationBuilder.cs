using System;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;
namespace Prism.NavigationEx
{
    public class NavigationBuilder : INavigationBuilder
    {
        private INavigation _firstNavigation;
        private INavigation _lastNavigation;

        public INavigationBuilder AddNavigation<TViewModel>() where TViewModel : INavigationViewModel
        {
            var navigation = new Navigation<TViewModel>();

            AddNavigation(navigation);

            return this;
        }

        public INavigationBuilder AddNavigation<TViewModel, TParameter>(TParameter parameter) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigation = new Navigation<TViewModel, TParameter>(parameter);

            AddNavigation(navigation);

            return this;
        }

        public IResultNavigationBuilder<TResult> AddReceivableNavigation<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived) where TViewModel : INavigationViewModel
        {
            var navigation = new ReceivableNavigation<TViewModel, TResult>(resultReceived);

            AddNavigation(navigation);

            return new ResultNavigationBuilder<TResult>(this);
        }

        public IResultNavigationBuilder<TResult> AddReceivableNavigation<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived) where TViewModel : INavigationViewModel<TParameter>
        {
            var navigation = new ReceivableNavigation<TViewModel, TParameter, TResult>(parameter, resultReceived);

            AddNavigation(navigation);

            return new ResultNavigationBuilder<TResult>(this);
        }

        private void AddNavigation(INavigation navigation)
        {
            if (_firstNavigation == null)
            {
                _firstNavigation = navigation;
            }

            if (_lastNavigation != null)
            {
                _lastNavigation.NextNavigation = navigation;
            }

            _lastNavigation = navigation;
        }

        public INavigation GetNavigation()
        {
            return _firstNavigation;
        }
    }
}
