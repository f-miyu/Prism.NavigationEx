using System;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public interface INavigation
    {
        Type ViewModelType { get; }
        object Parameter { get; }
        bool ParameterExists { get; }
        void OnResult(INavigationViewModel viewModel, INavigationParameters parameters);
    }
}
