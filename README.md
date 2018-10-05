# Prism.NavigationEx
Prism.NavigationEx provides ViewModel first navigation with Prism.Forms.

## Useage

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
                //...
            }
        });
    }
}

public class NextPageViewModel : NavigationViewModel<int, string>
{
    public DelegateCommand GoBackCommand { get; }

    public NextPageViewModel(INavigationService navigationService) : base(navigationService)
    {
        GoBackCommand = new DelegateCommand(() => GoBackAsync("result"));
    }

    public override void Prepare(int parameter)
    {
        //Initialize here
    }
}
```
### Wrap in NavigationPage
If wrapInNavigationPage parameter is true, thie next page is wrapped in NavigationPage.
```C#
await NavigateAsync<NextPageViewModel>(wrapInNavigationPage: true);
```

### Clear navigation stack
If noHistory parameter is true, navigation stack is cleared.
```C#
await NavigateAsync<NextPageViewModel>(noHistory: true);
```

### Confirm navigation
You can provide a canNavigate parameter which determines whether or not navigation can be done.
```C#
await NavigateAsync<NextPageViewModel>(canNavigate: () => pageDialogService.DisplayAlertAsync("title", "message", "OK", "Cancel");
```

### Deep Link
```C#

```
