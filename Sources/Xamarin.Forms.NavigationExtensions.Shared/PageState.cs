namespace Xamarin.Forms.NavigationExt
{
	using System;

	/// <summary>
	/// Represents a navigation state.
	/// </summary>
	public class PageState
	{
		/// <summary>
		/// Gets or sets the type of the page.
		/// </summary>
		/// <value>The type of the page.</value>
		public Type PageType { get; set; }

		/// <summary>
		/// Gets or sets the navigation argument.
		/// </summary>
		/// <value>The argument.</value>
		public object Argument { get; set; }
	}
}

