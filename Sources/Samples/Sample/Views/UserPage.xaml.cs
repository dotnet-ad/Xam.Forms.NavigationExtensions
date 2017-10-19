using System;
using System.Collections.Generic;
using Sample.Navigation;
using Xamarin.Forms;

namespace Sample
{
	public partial class UserPage : ContentPage
	{
		public UserPage()
		{
			this.BindingContext = new UserViewModel(Locator.Service);

			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			var userId = (int)this.GetNavigationArgs();

			this.Title = "Loading ...";

			try
			{
				var vm = this.BindingContext as UserViewModel;
				await vm.Update(userId);
				this.Title = userId.ToString();
			}
			catch (Exception ex)
			{
				this.Title = "Error";
			}
		}
	}
}

