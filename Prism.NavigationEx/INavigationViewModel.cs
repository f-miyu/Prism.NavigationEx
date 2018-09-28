using System;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public interface INavigationViewModel : INavigationAware, IDestructible, IConfirmNavigationAsync
    {
        void OnNavigatingFrom(INavigationParameters parameters);
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
