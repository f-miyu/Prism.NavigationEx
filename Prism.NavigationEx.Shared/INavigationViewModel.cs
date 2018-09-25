using System;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public interface INavigationViewModel : INavigationAware, IDestructible, IConfirmNavigationAsync
    {
        INavigationService NavigationService { get; }
        void OnNavigatingFrom(INavigationParameters parameters);
        Task<bool> CanNavigateAtNewAsync();
        Task<bool> CanNavigateAtBackAsync();
    }

    public interface INavigationViewModel<TParameter> : INavigationViewModel
    {
        void Prepare(TParameter parameter);
    }

    public interface INavigationViewModelResult<TResult> : INavigationViewModel
    {
    }

    public interface INavigationViewModel<TParameter, TResult> : INavigationViewModel<TParameter>, INavigationViewModelResult<TResult>
    {
    }
}
