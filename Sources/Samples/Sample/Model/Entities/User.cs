using System;
using Newtonsoft.Json;

namespace Sample
{
	public class User
	{
		[JsonProperty("id")]
		public int Identifier { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("website")]
		public string Website { get; set; }

		[JsonProperty("phone")]
		public string Phone { get; set; }
	}
}

