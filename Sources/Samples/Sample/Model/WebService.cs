using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sample.Navigation
{
	public class WebService : IService
	{
		public WebService()
		{
			this.Client = new HttpClient();
		}

		private HttpClient Client { get; set; }

		public const string EndPoint = "http://jsonplaceholder.typicode.com";

		public const string AlbumsPath = "albums";

		public const string AlbumPhotosPath = "photos?albumId={0}";

		public const string PhotoPath = "photos/{0}";

		public const string UsersPath = "users";

		public const string UserPath = "users/{0}";

		public Task<IEnumerable<Album>> GetAlbums()
		{
			return this.GetJson<IEnumerable<Album>>(AlbumsPath);
		}

		public Task<IEnumerable<Photo>> GetPhotos(int albumId)
		{
			return this.GetJson<IEnumerable<Photo>>(string.Format(AlbumPhotosPath,albumId));
		}

		public Task<Photo> GetPhoto(int photoId)
		{
			return this.GetJson<Photo>(string.Format(PhotoPath, photoId));
		}

		public Task<IEnumerable<User>> GetUsers()
		{
			return this.GetJson<IEnumerable<User>>(UsersPath);
		}

		public Task<User> GetUser(int userId)
		{
			return this.GetJson<User>(string.Format(UserPath, userId));
		}

		private async Task<T> GetJson<T>(string path)
		{
			var url = string.Format("{0}/{1}", EndPoint, path);
			var req = await this.Client.GetAsync(url);
			req.EnsureSuccessStatusCode();
			var content = await req.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(content);
		}
	}
}

