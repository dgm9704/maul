namespace UusiaAstia
{
	using System.Net.Http;
	using System.Net.Http.Json;

	public class Program
	{
		public static async Task Main(string[] args)
		{
			var oldUrl = args[0];
			Console.WriteLine(oldUrl);
			var kuid = oldUrl.Split('=').Last();
			Console.WriteLine(kuid);
			var astia = new Astia();
			await astia.GetNewUrl(kuid);
		}
	}

	public class Foo
	{
		public int ayid { get; set; }
		public string at3_ay_tunnus { get; set; } = string.Empty;

		public int jakso { get; set; }
	}

	public class Bar
	{
		public string searchString { get; set; } = string.Empty;
		public string searchTarget { get; set; } = string.Empty;
	}

	public class Astia
	{
		public async Task GetNewUrl(string kuid)
		{
			HttpClient http = new();

			var response = await http.GetAsync($"http://digi.narc.fi/fetchJaksoAndTunniste.php?kuid={kuid}");

			Foo foo = new();
			if (response.IsSuccessStatusCode)
			{
				foo = await response.Content.ReadFromJsonAsync<Foo>() ?? new Foo();
				Console.WriteLine(foo?.ToString());
			}
			else
			{
				string error = await response.Content.ReadAsStringAsync();
				Console.WriteLine(error);
				return;
			}

			Bar bar = new() { searchString = $"AY_{foo?.at3_ay_tunnus}", searchTarget = "aineisto", };
		}
	}
}