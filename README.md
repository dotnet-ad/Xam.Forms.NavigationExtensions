# NavigationExtensions for Xamarin.Forms

Those extensions for **Xamarin.Forms** basicaly add storage of the navigation history when the application stops. Extensions has been prefered to subclassing for more flexibility.

## Installation

The library **will** be available soon on [NuGet](https://www.nuget.org/packages/Xamarin.Forms.NavigationExtensions/).

To install NavigationExtensions, run the following command in the Package Manager Console.

	PM> Install-Package Xamarin.Forms.NavigationExtensions

## Quickstart

Once you installed the package, extension methods will be automaticaly added to `INavigation` and `Page`.

To add storage and restoration of navigation stack :

```csharp
public class App : Application
{
    public App()
    {
        MainPage = new NavigationPage(new HomePage());
    }

    protected override async void OnStart()
    {
        // If the app is launched one hour after its last stop, its navigation history 
        // will be restored.
        await this.MainPage.Navigation.RestoreAsync("Main", TimeSpan.FromHours(1));
    }

    protected override void OnSleep()
    {
        this.MainPage.Navigation.Store("Main");
    }
}
```

To navigate from a page to another by passing arguments :

```csharp
this.Navigation.PushAsync<AlbumPage>(item.Identifier, true);
```

And finaly access navigation argument from your destination page :

```csharp
var albumId = (int) this.GetNavigationArgs();
```

## Sample

A complete application sample is available in the source code solution.

## Copyright and license

Code and documentation copyright 2016 Aloïs Deniel released under the MIT license.