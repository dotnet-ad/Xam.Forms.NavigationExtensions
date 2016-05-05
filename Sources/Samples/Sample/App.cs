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

			MainPage = new NavigationPage(new HomePage());
		}

		protected override async void OnStart()
		{
			await this.MainPage.Navigation.RestoreAsync("Main", TimeSpan.FromHours(1));
		}

		protected override void OnSleep()
		{
			this.MainPage.Navigation.Store("Main");
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

