namespace Xamarin.Forms.NavigationExtensions
{
	using System;

	public class Navigators
	{
		#region Attached properties

		public static BindableProperty NavigatorProperty = BindableProperty.CreateAttached( "Navigator", typeof(INavigator), typeof(Navigators), null, BindingMode.OneWay, null, HandleBindingPropertyChangedDelegate);

		private static void HandleBindingPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
		{
			var app = bindable as Application;
			var nav = newValue as INavigator;


			if (app != null && nav != null)
			{

			}
			else
			{
				throw new ArgumentException("The property should only be attached to an application and should be a Navigator.");
			}
		}

		#endregion

		#region Constructors

		public Navigators()
		{
			
		}

		#endregion
	}
}

