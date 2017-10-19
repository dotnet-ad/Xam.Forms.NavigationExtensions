namespace Sample.Navigation
{
	using Newtonsoft.Json;

	public class Photo
	{
		[JsonProperty("id")]
		public int Identifier { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("thumbnailUrl")]
		public string ThumbnailUrl { get; set; }
	}
}

