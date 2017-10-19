using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Sample.Navigation
{
	public partial class AlbumPage : ContentPage
	{
		public AlbumPage()
		{
			this.BindingContext = new AlbumViewModel(Locator.Service);

			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			var albumId = (int) this.GetNavigationArgs();

			this.Title = "Loading ...";

			try
			{
				var vm = this.BindingContext as AlbumViewModel;
				await vm.Update(albumId);
				this.Title = albumId.ToString();
			}
			catch (Exception ex)
			{
				this.Title = "Error";
			}
		}

		private void OnItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			var item = e.Item as Photo;
			this.Navigation.PushAsync<PhotoPage>(item.Identifier, true);
		}
	}
}

