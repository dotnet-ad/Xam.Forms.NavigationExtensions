namespace Xamarin.Forms
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.CompilerServices;
	using System.Threading.Tasks;
	using Xamarin.Forms;
	using System.Linq;
	using Newtonsoft.Json;

	/// <summary>
	/// Extensions for saving and restoring navigation states with arguments.
	/// </summary>
	public static class NavigationExtensions
	{
		#region Arguments access

		/// <summary>
		/// All the navigation arguments associated to pages.
		/// </summary>
		private static ConditionalWeakTable<Page, object> arguments = new ConditionalWeakTable<Page, object>();

		/// <summary>
		/// Gets the navigation arguments for a page.
		/// </summary>
		/// <returns>The navigation arguments.</returns>
		/// <param name="page">Page.</param>
		public static object GetNavigationArgs(this Page page)
		{
			object argument = null;
			arguments.TryGetValue(page, out argument);

			return argument;
		}

		/// <summary>
		/// Stores the navigation arguments for a page.
		/// </summary>
		/// <returns>The navigation arguments.</returns>
		/// <param name="page">Page.</param>
		/// <param name="args">Arguments.</param>
		public static void SetNavigationArgs(this Page page, object args)
		{
			arguments.Add(page, args);
		}

		#endregion

		#region State restoration

		/// <summary>
		/// Represents a navigation state.
		/// </summary>
		public class StoredStates
		{
			public DateTime Date { get; set; }

			public IEnumerable<State> Navigation { get; set; }

			public IEnumerable<State> Modal { get; set; }
		}

		/// <summary>
		/// Represents a navigation state.
		/// </summary>
		public class State
		{
			/// <summary>
			/// Gets or sets the type of the page.
			/// </summary>
			/// <value>The type of the page.</value>
			public Type PageType { get; set; }

			/// <summary>
			/// Gets or sets the type of the navigation argument.
			/// </summary>
			/// <value>The type of the argument.</value>
			public Type ArgumentType { get; set; }

			/// <summary>
			/// Gets or sets the serialized argument.
			/// </summary>
			/// <value>The serialized argument.</value>
			public string SerializedArgument { get; set; }

			/// <summary>
			/// Gets or sets the argument.
			/// </summary>
			/// <value>The argument.</value>
			[JsonIgnore]
			public object Argument
			{
				get
				{
					if (string.IsNullOrEmpty(this.SerializedArgument) || this.ArgumentType == null)
					{
						return null;
					}

					return JsonConvert.DeserializeObject(SerializedArgument, ArgumentType);
				}
				set
				{
					if (value != null)
					{
						this.ArgumentType = value.GetType();
						this.SerializedArgument = JsonConvert.SerializeObject(value);
					}
					else
					{
						this.ArgumentType = null;
						this.SerializedArgument = null;
					}

				}
			}
		}

		private const string StoreKeyPrefix = "Navigation.";

		/// <summary>
		/// Restores all stacks from saved state in local storage.
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="navigation">Navigation.</param>
		public static async Task RestoreAsync(this INavigation navigation, string name, TimeSpan maximumRestoreSpan)
		{
			var key = $"{StoreKeyPrefix}{name}";

			if (Application.Current.Properties.ContainsKey(key))
			{
				var json = Application.Current.Properties[key] as string;
				var states = JsonConvert.DeserializeObject<StoredStates>(json);

				if (DateTime.Now - states.Date < maximumRestoreSpan)
				{
					var navigationPages = states.Navigation.Select(RestorePage).ToList();
					var modalPages = states.Modal.Select(RestorePage).ToList();

					var initialPages = navigation.NavigationStack.ToList();

					// 1. Restoring navigation stack
					var lastNavigationPage = navigationPages.LastOrDefault();
					navigationPages.RemoveAt(navigationPages.Count - 1);
					await navigation.PushAsync(lastNavigationPage, false);
					foreach (var page in navigationPages)
					{
						navigation.InsertPageBefore(page, lastNavigationPage);
					}

					// 2. Removing already present pages before restore
					foreach (var page in initialPages)
					{
						navigation.RemovePage(page);
					}


					//3. Restoring modal stack
					foreach (var page in modalPages)
					{
						await navigation.PushModalAsync(page, false);
						// HACK: lack of InsertPageBefore for modal stack
						if (Device.OS == TargetPlatform.iOS)
						{
							await Task.Delay(100);
						}
					}
				}
			}
		}

		/// <summary>
		/// Stores all the navigations stacks with the given name into local storage.
		/// </summary>
		/// <param name="navigation">The navigation service.</param>
		/// <param name="name">The name that will represents this navigation.</param>
		public static void Store(this INavigation navigation, string name)
		{
			var states = new StoredStates()
			{
				Date = DateTime.Now,
				Navigation = ConvertPages(navigation.NavigationStack),
				Modal = ConvertPages(navigation.ModalStack),
			};

			var json = JsonConvert.SerializeObject(states);
			Application.Current.Properties[$"{StoreKeyPrefix}{name}"] = json;
		}

		/// <summary>
		/// Restores the page by instanciating the page and argument from stored state.
		/// </summary>
		/// <returns>The page.</returns>
		/// <param name="state">State.</param>
		private static Page RestorePage(State state)
		{
			var page = Activator.CreateInstance(state.PageType) as Page;
			var argument = state.Argument;
			page.SetNavigationArgs(argument);
			return page;
		}
			                                       
		private static IEnumerable<State> ConvertPages(IEnumerable<Page> pages)
		{
			return pages.Select((p) => new State()
			{
				PageType = p.GetType(),
				Argument = p.GetNavigationArgs(),
			});
		}

		#endregion

		#region Navigation

		public static Task PushAsync(this INavigation navigation, Page page, object args, bool animated)
		{
			page.SetNavigationArgs(args);
			return navigation.PushAsync(page, animated);
		}

		public static Task PushModalAsync(this INavigation navigation, Page page, object args, bool animated)
		{
			page.SetNavigationArgs(args);
			return navigation.PushModalAsync(page, animated);
		}

		public static Task PushAsync(this INavigation navigation, Type pageType, object args = null, bool animated = true)
		{
			var page = Activator.CreateInstance(pageType) as Page;
			return navigation.PushAsync(page, args, animated);
		}

		public static Task PushModalAsync(this INavigation navigation, Type pageType, object args = null, bool animated = true)
		{
			var page = Activator.CreateInstance(pageType) as Page;
			return navigation.PushAsync(page, args, animated);
		}

		public static Task PushAsync<T>(this INavigation navigation, object args = null, bool animated = true) where T : Page
		{
			return navigation.PushAsync(typeof(T),args,animated);
		}

		public static Task PushModalAsync<T>(this INavigation navigation, object args = null, bool animated = true) where T : Page
		{
			return navigation.PushAsync(typeof(T), args, animated);
		}

		#endregion
	}
}