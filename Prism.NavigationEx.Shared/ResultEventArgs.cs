using System;
using Prism.Events;
namespace Prism.NavigationEx
{
    public class ResultEventArgs<TViewModel, TResult> : EventArgs
    {
        public TViewModel ViewModel { get; }
        public TResult Result { get; }

        public ResultEventArgs(TViewModel viewModel, TResult result)
        {
            ViewModel = viewModel;
            Result = result;
        }
    }
}
