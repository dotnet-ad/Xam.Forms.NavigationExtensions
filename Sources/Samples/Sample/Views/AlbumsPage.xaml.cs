using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Sample.Navigation
{
	public partial class AlbumsPage : ContentPage
	{
		public AlbumsPage()
		{
			this.BindingContext = new AlbumsViewModel(Locator.Service);

			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			this.Title = "Loading ...";

			try
			{
				var vm = this.BindingContext as AlbumsViewModel;
				await vm.Update();
				this.Title = "Albums";
			}
			catch (Exception ex)
			{
				this.Title = "Error";
			}
		}

		private void OnItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			var item = e.Item as Album;
			this.Navigation.PushAsync<AlbumPage>(item.Identifier, true);
		}
	}
}

