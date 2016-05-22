using System;
using System.Collections.Generic;

namespace Xamarin.Forms.NavigationExtensions
{
	public class Section
	{
		public string Title
		{
			get;
			set;
		}	

		public IEnumerable<Type> Pages
		{
			get;
			set;
		}
	}
}