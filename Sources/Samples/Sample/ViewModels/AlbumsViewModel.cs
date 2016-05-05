using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Sample.Navigation
{
	public class AlbumsViewModel : ViewModelBase
	{
		public AlbumsViewModel(IService service)
		{
			this.service = service;
			this.Albums = new List<Album>();
		}

		private readonly IService service;

		private IEnumerable<Album> albums;

		public IEnumerable<Album> Albums
		{
			get { return albums; }
			set { this.Set(ref albums, value); }
		}

		public async Task Update()
		{
			if (!this.Albums.Any())
			{
				this.Albums = await this.service.GetAlbums();
			}
		}
	}
}

