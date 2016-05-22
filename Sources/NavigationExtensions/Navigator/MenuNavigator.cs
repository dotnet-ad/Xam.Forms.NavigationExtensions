namespace Xamarin.Forms.NavigationExtensions
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Creates a navigator that creates a list of menu items grouped by sections in a single page.
	/// </summary>
	public class MenuNavigator : INavigator
	{
		public MenuNavigator()
		{
			
		}

		#region Properties

		public DataTemplate SectionHeaderTemplate
		{
			get;
			set;
		}

		public DataTemplate SectionItemTemplate
		{
			get;
			set;
		}

		#endregion

		#region Events

		public event EventHandler ItemSelected;

		#endregion

		#region Methods

		public Page CreateRoot(IEnumerable<Section> sections)
		{
			var listView = new ListView()
			{
				IsGroupingEnabled = true,
				SeparatorVisibility = SeparatorVisibility.None,
				GroupHeaderTemplate = this.SectionHeaderTemplate,
				ItemTemplate = this.SectionItemTemplate,
				ItemsSource = sections.ToList(),
			};


			return new ContentPage()
			{
				Content = listView,
			};
		}

		#endregion
	}
}

