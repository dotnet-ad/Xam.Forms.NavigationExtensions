using System;
using System.Collections.Generic;

namespace Xamarin.Forms.NavigationExtensions
{
	/// <summary>
	/// A navigator that chooses the best navigator for the current OS and Idiom.
	/// </summary>
	public class AutoNavigator : INavigator
	{
		public AutoNavigator()
		{
		}

		private INavigator sidemenu = new SideMenuNavigator();

		private INavigator tabbed = new TabbedNavigator();

		public Page CreateRoot(IEnumerable<Section> sections)
		{
			if (Device.OS == TargetPlatform.Android)
			{
				return this.sidemenu.CreateRoot(sections);
			}

			return this.tabbed.CreateRoot(sections);
		}
	}
}

