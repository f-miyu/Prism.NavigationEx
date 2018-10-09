# Prism.NavigationEx
Prism.NavigationEx provides ViewModel first navigation for [Prism.Forms](https://github.com/PrismLibrary/Prism).

## Setup
Install NuGet package.

[Prism.NavigationEx](https://www.nuget.org/packages/Prism.NavigationEx/) [![NuGet](https://img.shields.io/nuget/v/Prism.NavigationEx.svg?label=NuGet)](https://www.nuget.org/packages/Prism.NavigationEx/)

## Usage
You can specify targert ViewModel and parameter type for navigation.
```C#
public class MainPageViewModel : NavigationViewModel
{
    public DelegateCommand GoToNextCommand { get; }

    public MainPageViewModel(INavigationService navigationService) : base(navigationService)
    {
        GoToNextCommand = new DelegateCommand(() => NavigateAsync<NextPageViewModel, int>(100));
    }
}

public class NextPageViewModel : NavigationViewModel<int>
{
    private int _parameter;
    
    public DelegateCommand GoBackCommand { get; }

    public NextPageViewModel(INavigationService navigationService) : base(navigationService)
    {
        GoBackCommand = new DelegateCommand(() => GoBackAsync());
    }

    public override void Prepare(int parameter)
    {
        _parameter = parameter;
    }
}
```

If you want to return result, you can do as follows.
```C#
public class MainPageViewModel : NavigationViewModel
{
    public DelegateCommand GoToNextCommand { get; }

    public MainPageViewModel(INavigationService navigationService) : base(navigationService)
    {
        GoToNextCommand = new DelegateCommand(async () =>
        {
            var result = await NavigateAsync<NextPageViewModel, int, string>(100);
            if (result.Success)
            {
                var data = result.Data;
                //...
            }
        });
    }
}

public class NextPageViewModel : NavigationViewModel<int, string>
{
    private int _parameter;
    
    public DelegateCommand GoBackCommand { get; }

    public NextPageViewModel(INavigationService navigationService) : base(navigationService)
    {
        GoBackCommand = new DelegateCommand(() => GoBackAsync("result"));
    }

    public override void Prepare(int parameter)
    {
        _parameter = parameter;
    }
}
```
If you don't need initializing, you can use `NavigationViewModelResult`.

### Wrap in NavigationPage
If `wrapInNavigationPage` argument is true, thie next page is wrapped in NavigationPage.
```C#
NavigateAsync<NextPageViewModel>(wrapInNavigationPage: true);
```

### Clear navigation stack
If `noHistory` argument is true, navigation stack is cleared.
```C#
NavigateAsync<NextPageViewModel>(noHistory: true);
```

### Confirm navigation
You can provide a `canNavigate` argument which determines whether or not navigation can be done.
```C#
NavigateAsync<NextPageViewModel>(canNavigate: () => pageDialogService.DisplayAlertAsync("title", "message", "OK", "Cancel");
```

### Replace current page
If `replaced` argument is true, current page is replaced.
```C#
NavigateAsync<NextPageViewModel>(replaced: true);
```

### Deep Link
This library supports deep link.
```C#
var navigation = NavigationFactory.Create<MainPageViewModel>()
                                  .Add<NextPageViewModel, int>(100);
                                  .Add<NextNextPageViewModel, int>(200);

NavigateAsync(navigation, wrapInNavigationPage: true);
```

If you want to return result in the middle of link, you can do as follows. 
```C#
var navigation = NavigationFactory.Create<MainPageViewModel, string>((viewModel, result) => 
                                  {
                                      if (result.Success && viewModel is MainPageViewModel mainPageViewModel)
                                      {
                                          var data = result.Data;
                                          //...
                                      }
                                  })
                                  .Add<NextPageViewModel, int, string>(100, (viewModel, result) => 
                                  {
                                      if (result.Success && viewModel is NextPageViewModel nextPageViewModel)
                                      {
                                          var data = result.Data;
                                          //...
                                      }
                                  })
                                  .Add<NextNextPageViewModel, int>(200);

NavigateAsync(navigation, wrapInNavigationPage: true);
```

### TabbedPage
You can create tab by specifying ViewModel and parameter type. If `wrapInNavigationPage` is true, the tab is wrapped in NavigationPage.
```C#
var navigation = NavigationFactory.Create<MyTabbedPageViewModel>(null, new Tab<FirstTabPageViewModel, string>("text", true), new Tab<SecondTabPageViewModel>());

NavigateAsync(navigation, noHistory: true);
```

### Registering all pages
This library provides the way of registering your all pages and NavigationPage.
```C#
protected override void RegisterTypes(IContainerRegistry containerRegistry)
{
    containerRegistry.RegisterForNavigation(this);
}
```

### NavigationNameProvider
By default, a target page name for a ViewModel is the ViewModel name which "ViewModel"　is removed from. If you want to use other pages, you can customize it.
```C#
NavigationNameProvider.SetDefaultViewModelTypeToNavigationNameResolver(viewModelType =>
{
    //...
});
```
If you want to use other NavigationPage, you can change it.
```C#
NavigationNameProvider.DefaultNavigationPageName = nameof(MyNavigationPage);
```
