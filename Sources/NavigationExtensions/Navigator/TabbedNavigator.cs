namespace Xamarin.Forms.NavigationExtensions
{
	using System;
	using System.Linq;
	using System.Collections.Generic;

	/// <summary>
	/// A navigator that creates a tab for each page of the first section and all the others in a menu in a last tab.
	/// </summary>
	public class TabbedNavigator : INavigator
	{
		public TabbedNavigator()
		{
			this.Menu = new MenuNavigator();
		}

		#region Properties

		public INavigator Menu
		{
			get;
			set;
		}

		#endregion

		#region Methods

		public Page CreateRoot(IEnumerable<Section> sections)
		{
			var root = new TabbedPage();

			// Adding tabs from first section

			var firstSection = sections.FirstOrDefault();

			if (firstSection != null)
			{
				foreach (var pageType in firstSection.Pages)
				{
					var page = Activator.CreateInstance(pageType) as Page;

					var navigationPage = new NavigationPage(page)
					{
						Title = page.Title,
						Icon = page.Icon,
					};

					root.Children.Add(navigationPage);
				}
			}

			// Adding the others in the last tab

			var otherSections = sections.Skip(1);

			if (otherSections.Any())
			{
				var menuPage = this.Menu.CreateRoot(otherSections);

				var menuNavPage = new NavigationPage(menuPage)
				{
					Title = menuPage.Title,
					Icon = menuPage.Icon,
				};

				root.Children.Add(menuNavPage);
			}
			  
			return root;
		}

		#endregion
	}
}

