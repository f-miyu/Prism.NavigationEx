using System;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Prism.NavigationEx
{
    public class ReceivableTabbedNavigationPath<TViewModel, TResult> : ReceivableNavigationPath<TViewModel, TResult> where TViewModel : INavigationViewModel
    {
        protected ITab[] _tabs;

        public ReceivableTabbedNavigationPath(ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) : base(resultReceived, canNavigate)
        {
            _tabs = tabs;
        }

        public override string GetPath(NavigationParameters parameters, NavigationParameters queries = null, NavigationParameters nextQueries = null)
        {
            TabbedNavigationPathHelper.SetParameters(ref parameters, ref queries, _tabs);
            return base.GetPath(parameters, queries, nextQueries);
        }
    }

    public class ReceivableTabbedNavigationPath<TViewModel, TParameter, TResult> : ReceivableNavigationPath<TViewModel, TParameter, TResult> where TViewModel : INavigationViewModel<TParameter>
    {
        protected ITab[] _tabs;

        public ReceivableTabbedNavigationPath(TParameter parameter, ResultReceivedDelegate<TViewModel, TResult> resultReceived, Func<Task<bool>> canNavigate = null, params ITab[] tabs) : base(parameter, resultReceived, canNavigate)
        {
            _tabs = tabs;
        }

        public override string GetPath(NavigationParameters parameters, NavigationParameters queries = null, NavigationParameters nextQueries = null)
        {
            TabbedNavigationPathHelper.SetParameters(ref parameters, ref queries, _tabs);
            return base.GetPath(parameters, queries, nextQueries);
        }
    }
}
