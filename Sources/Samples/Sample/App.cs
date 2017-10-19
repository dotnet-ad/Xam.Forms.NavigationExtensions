using System;

using Xamarin.Forms;
using Sample.Navigation;

namespace Sample
{
	public class App : Application
	{
		public App()
		{
			Locator.Service = new WebService();

			var tabbedPage = new TabbedPage();

			tabbedPage.Children.Add(new NavigationPage(new AlbumsPage()) { Title="Albums" });
			tabbedPage.Children.Add(new NavigationPage(new UsersPage()) { Title="Users" });

			MainPage = tabbedPage;
		}

		public readonly TimeSpan RestorationTimeSpan = TimeSpan.FromHours(1);

		protected override async void OnStart()
		{
			var tabbedPage = this.MainPage as TabbedPage;
			await tabbedPage.Children.RestoreAsync("Main", RestorationTimeSpan);
		}

		protected override void OnSleep()
		{
			var tabbedPage = this.MainPage as TabbedPage;
			tabbedPage.Children.Store("Main");
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

