using System;
using System.Collections.Generic;
using Sample.Navigation;
using Xamarin.Forms;

namespace Sample
{
	public partial class UsersPage : ContentPage
	{
		public UsersPage()
		{
			this.BindingContext = new UsersViewModel(Locator.Service);

			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			this.Title = "Loading ...";

			try
			{
				var vm = this.BindingContext as UsersViewModel;
				await vm.Update();
				this.Title = "Users";
			}
			catch (Exception ex)
			{
				this.Title = "Error";
			}
		}

		private void OnItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			var item = e.Item as User;
			this.Navigation.PushAsync<UserPage>(item.Identifier, true);
		}
	}
}

