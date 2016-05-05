using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Sample.Navigation;

namespace Sample
{
	public class UsersViewModel : ViewModelBase
	{
		public UsersViewModel(IService service)
		{
			this.service = service;
			this.Users = new List<User>();
		}

		private readonly IService service;

		private IEnumerable<User> users;

		public IEnumerable<User> Users
		{
			get { return users; }
			set { this.Set(ref users, value); }
		}

		public async Task Update()
		{
			if (!this.Users.Any())
			{
				this.Users = await this.service.GetUsers();
			}
		}
	}
}

