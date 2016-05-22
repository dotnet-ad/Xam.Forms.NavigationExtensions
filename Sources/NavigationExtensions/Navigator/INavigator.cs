using System;
using System.Collections.Generic;

namespace Xamarin.Forms.NavigationExtensions
{
	public interface INavigator
	{
		Page CreateRoot(IEnumerable<Section> sections);
	}
}

