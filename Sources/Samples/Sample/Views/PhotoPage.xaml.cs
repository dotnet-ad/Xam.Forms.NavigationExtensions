using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Sample.Navigation
{
	public partial class PhotoPage : ContentPage
	{
		public PhotoPage()
		{
			this.BindingContext = new PhotoViewModel(Locator.Service);

			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			var photoId = (int)this.GetNavigationArgs();

			this.Title = "Loading ...";

			try
			{
				var vm = this.BindingContext as PhotoViewModel;
				await vm.Update(photoId);
				this.Title = photoId.ToString();
			}
			catch (Exception ex)
			{
				this.Title = "Error";
			}
		}
	}
}

