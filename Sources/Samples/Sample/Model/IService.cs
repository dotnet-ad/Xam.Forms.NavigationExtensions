using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Navigation
{
	public interface IService
	{
		Task<IEnumerable<Album>> GetAlbums();

		Task<IEnumerable<Photo>> GetPhotos(int albumId);

		Task<Photo> GetPhoto(int photoId);

		Task<IEnumerable<User>> GetUsers();

		Task<User> GetUser(int userId);
	}
}

