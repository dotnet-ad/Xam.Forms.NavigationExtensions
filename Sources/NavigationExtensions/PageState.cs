namespace Xamarin.Forms.NavigationExt
{
	using System;
	using Newtonsoft.Json;

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
}

