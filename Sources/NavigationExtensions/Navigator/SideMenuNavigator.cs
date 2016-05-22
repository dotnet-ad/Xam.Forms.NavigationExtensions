using System;
using System.Collections.Generic;

namespace Xamarin.Forms.NavigationExtensions
{
	/// <summary>
	/// A navigator that creates a side menu that contains a menu for choosing the current page.
	/// </summary>
	public class SideMenuNavigator : INavigator
	{
		public SideMenuNavigator()
		{
			this.Menu = new MenuNavigator();
		}

		public INavigator Menu
		{
			get;
			set;
		}

		public Page CreateRoot(IEnumerable<Section> sections)
		{
			var root = new MasterDetailPage();

			root.Master = this.Menu.CreateRoot(sections);

			return root;
		}
	}
}

