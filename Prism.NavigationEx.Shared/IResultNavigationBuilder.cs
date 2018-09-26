using System;
namespace Prism.NavigationEx
{
    public interface IResultNavigationBuilder<TReceivedResult>
    {
        INavigationBuilder AddNavigation<TViewModel>() where TViewModel : INavigationViewModelResult<TReceivedResult>;
        INavigationBuilder AddNavigation<TViewModel, TParameter>(TParameter parameter) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>;
        IResultNavigationBuilder<TResult> AddNavigation<TViewModel, TResult>(ResultReceivedDelegate<TViewModel, TResult> resultReceived) where TViewModel : INavigationViewModelResult<TReceivedResult>;
        IResultNavigationBuilder<TResult> AddNavigation<TViewModel, TParameter, TResult>(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived) where TViewModel : INavigationViewModel<TParameter, TReceivedResult>;
        INavigation GetNavigation();
    }
}
