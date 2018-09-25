using System;
namespace Prism.NavigationEx
{
    public delegate void ResultDelegate<TResult>(INavigationViewModel viewModel, TResult result);
}
