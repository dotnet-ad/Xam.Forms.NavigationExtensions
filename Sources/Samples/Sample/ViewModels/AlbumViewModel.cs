using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Sample.Navigation
{
	public class AlbumViewModel : ViewModelBase
	{
		public AlbumViewModel(IService service)
		{
			this.service = service;
			this.Photos = new List<Photo>();
		}

		private int albumId;

		private readonly IService service;

		private IEnumerable<Photo> photos;

		public IEnumerable<Photo> Photos
		{
			get { return photos; }
			set { this.Set(ref photos, value); }
		}

		public async Task Update(int albumId)
		{
			if (albumId != this.albumId || !this.Photos.Any())
			{
				this.albumId = albumId;
				this.Photos = await this.service.GetPhotos(albumId);
			}
		}
	}
}

