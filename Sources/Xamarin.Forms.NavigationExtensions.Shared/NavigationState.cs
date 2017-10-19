namespace Xamarin.Forms.NavigationExt
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Represents a navigation state.
	/// </summary>
	public class NavigationStates
	{
		/// <summary>
		/// Gets or sets the store date.
		/// </summary>
		/// <value>The date.</value>
		public DateTime Date { get; set; }

		/// <summary>
		/// Gets or sets the all the stored navigation states (page and args).
		/// </summary>
		/// <value>The navigation stack.</value>
		public IEnumerable<PageState> Navigation { get; set; }

		/// <summary>
		/// Gets or sets the all the stored modal navigation states (page and args).
		/// </summary>
		/// <value>The modal stack.</value>
		public IEnumerable<PageState> Modal { get; set; }
	}
}

