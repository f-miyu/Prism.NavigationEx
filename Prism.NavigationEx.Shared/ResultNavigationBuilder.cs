using System;
namespace Prism.NavigationEx
{
    internal class ResultNavigationBuilder<TReceivedResult> : IResultNavigationBuilder<TReceivedResult>
    {
        private readonly INavigationBuilder _navigationBuilder;

        public ResultNavigationBuilder(INavigationBuilder navigationBuilder)
        {
            _navigationBuilder = navigationBuilder;
        }

        public INavigationBuilder AddNavigation<TViewModel>() where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigationBuilder.AddNavigation<TViewModel>();
        }

        public INavigationBuilder AddNavigation<TViewModel, TParameter>(TParameter parameter) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigationBuilder.AddNavigation<TViewModel, TParameter>(parameter);
        }

        public IResultNavigationBuilder<TResult> AddNavigation<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived) where TViewModel : INavigationViewModelResult<TReceivedResult>
        {
            return _navigationBuilder.AddNavigation<TViewModel, TResult>(resultReceived);
        }

        public IResultNavigationBuilder<TResult> AddNavigation<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>
        {
            return _navigationBuilder.AddNavigation<TViewModel, TParameter, TResult>(parameter, resultReceived);
        }

        public INavigation GetNavigation()
        {
            return _navigationBuilder.GetNavigation();
        }
    }
}
