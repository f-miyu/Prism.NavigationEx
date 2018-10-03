using System;
using System.Collections.Generic;
using Prism.Navigation;
using System.Linq;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Prism.NavigationEx
{
    public class TabbedNavigationPath : NavigationPath
    {
        protected ITab[] _tabs;

        public TabbedNavigationPath(params ITab[] tabs) : this(nameof(TabbedPage), tabs)
        {
        }

        public TabbedNavigationPath(string path, params ITab[] tabs) : base(path)
        {
            _tabs = tabs;
        }

        public override string GetPath(NavigationParameters parameters, NavigationParameters queries = null, NavigationParameters nextQueries = null)
        {
            TabbedNavigationPathHelper.SetParameters(ref parameters, ref queries, _tabs);
            return base.GetPath(parameters, queries, nextQueries);
        }
    }

    public class TabbedNavigationPath<TViewModel> : NavigationPath<TViewModel> where TViewModel : INavigationViewModel
    {
        protected ITab[] _tabs;

        public TabbedNavigationPath(Func<Task<bool>> canNavigate = null, params ITab[] tabs) : base(canNavigate)
        {
            _tabs = tabs;
        }

        public override string GetPath(NavigationParameters parameters, NavigationParameters queries = null, NavigationParameters nextQueries = null)
        {
            TabbedNavigationPathHelper.SetParameters(ref parameters, ref queries, _tabs);
            return base.GetPath(parameters, queries, nextQueries);
        }
    }

    public class TabbedNavigationPath<TViewModel, TParameter> : NavigationPath<TViewModel, TParameter> where TViewModel : INavigationViewModel<TParameter>
    {
        protected ITab[] _tabs;

        public TabbedNavigationPath(TParameter parameter, Func<Task<bool>> canNavigate = null, params ITab[] tabs) : base(parameter, canNavigate)
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
