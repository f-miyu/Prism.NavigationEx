using System;
namespace Prism.NavigationEx
{
    public interface INavigationBuilder
    {
        INavigationBuilder AddNavigation<TViewModel>() where TViewModel : INavigationViewModel;
        INavigationBuilder AddNavigation<TViewModel, TParameter>(TParameter parameter) where TViewModel : INavigationViewModel<TParameter>;
        IResultNavigationBuilder<TResult> AddReceivableNavigation<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived) where TViewModel : INavigationViewModel;
        IResultNavigationBuilder<TResult> AddReceivableNavigation<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived) where TViewModel : INavigationViewModel<TParameter>;
        INavigation GetNavigation();
    }
}
