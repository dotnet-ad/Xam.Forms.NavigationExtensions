namespace Sample.Navigation
{
	using Newtonsoft.Json;

	public class Album
	{
		[JsonProperty("id")]
		public int Identifier { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }
	}
}

