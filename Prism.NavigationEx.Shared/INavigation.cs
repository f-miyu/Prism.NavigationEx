using System;
namespace Prism.NavigationEx
{
    public interface INavigation
    {
        Type ViewModelType { get; }
        object Parameter { get; }
        bool ParameterExists { get; }
    }
}
