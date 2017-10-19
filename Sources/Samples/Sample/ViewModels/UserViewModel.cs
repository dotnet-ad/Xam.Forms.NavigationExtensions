using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Sample.Navigation;

namespace Sample
{
	public class UserViewModel : ViewModelBase
	{
		public UserViewModel(IService service)
		{
			this.service = service;
		}

		private int userId = -1;

		private readonly IService service;

		private User user;

		public User User
		{
			get { return user; }
			set 
			{
				if (this.Set(ref user, value))
				{
					this.RaisePropertyChanged(() => this.Name);
					this.RaisePropertyChanged(() => this.Username);
					this.RaisePropertyChanged(() => this.Phone);
					this.RaisePropertyChanged(() => this.Website);
					this.RaisePropertyChanged(() => this.Email);
				}
			}
		}

		public string Name
		{
			get { return this.user?.Name; }
		}

		public string Username
		{
			get { return this.user?.Username; }
		}

		public string Phone
		{
			get { return this.user?.Phone; }
		}

		public string Email
		{
			get { return this.user?.Email; }
		}

		public string Website
		{
			get { return this.user?.Website; }
		}

		public async Task Update(int userId)
		{
			if (userId != this.userId)
			{
				this.userId = userId;
				this.User = await this.service.GetUser(userId);
			}
		}
	}
}

