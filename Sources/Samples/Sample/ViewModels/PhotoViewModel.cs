using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Sample.Navigation
{
	public class PhotoViewModel : ViewModelBase
	{
		public PhotoViewModel(IService service)
		{
			this.service = service;
		}

		private int photoId = -1;

		private readonly IService service;

		private string url;

		public string Url
		{
			get { return url; }
			set { this.Set(ref url, value); }
		}

		public async Task Update(int photoId)
		{
			if (photoId != this.photoId)
			{
				this.photoId = photoId;
				this.Url = (await this.service.GetPhoto(photoId))?.Url;
			}
		}
	}
}

