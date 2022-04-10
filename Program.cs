namespace UusiaAstia
{
	using System;
	using System.Dynamic;
	using System.Net.Http;
	using System.Net.Http.Json;
	using System.Text.Json.Nodes;

	public class Program
	{
		public static async Task Main(string[] args)
		{
			var oldUrl = "http://digi.narc.fi/digi/view.ka?kuid=7137029"; //args[0];
			Console.WriteLine(oldUrl);
			var kuid = oldUrl.Split('=').Last();
			Console.WriteLine(kuid);
			var astia = new Astia();
			await astia.GetNewUrl(kuid);
		}
	}

	public class JaksoAndTunniste
	{
		public int ayid { get; set; }
		public string at3_ay_tunnus { get; set; } = string.Empty;

		public int jakso { get; set; }
	}

	public class TietoQuery
	{
		public string searchString { get; set; } = string.Empty;
		public string searchTarget { get; set; } = string.Empty;
	}

	public class Astia
	{
		private HttpClient http = new();

		public async Task GetNewUrl(string kuid)
		{
			//kuid = "7137029";
			//var jaksoAndtunniste = await GetJaksoAndTunniste(kuid);
			var jaksoAndtunniste = new JaksoAndTunniste { at3_ay_tunnus = "1294995.KA", ayid = 114889, jakso = 102 };

			//var id = await GetAineistoId(jaksoAndtunniste);
			var id = "1193635722";

			string tiedosto = await GetTiedosto(jaksoAndtunniste, id);


		}

		private async Task<string> GetTiedosto(JaksoAndTunniste jaksoAndtunniste, string id)
		{
			var response = await http.GetAsync($"https://astia.narc.fi/uusiastia/json/json_tiedostot.php?id={id}");
			string json = string.Empty;
			if (response.IsSuccessStatusCode)
			{
				var tiedostoResult = await response.Content.ReadFromJsonAsync<ExpandoObject>() ?? new ExpandoObject();
				json = await response.Content.ReadAsStringAsync();
			}
			else
			{
				string error = await response.Content.ReadAsStringAsync();
				throw new Exception(error);
			}

			return "";

		}

		private async Task<string> GetAineistoId(JaksoAndTunniste jaksoAndTunniste)
		{
			TietoQuery tietoQuery = new() { searchString = $"AY_{jaksoAndTunniste?.at3_ay_tunnus}", searchTarget = "aineisto" };

			var response = await http.PostAsJsonAsync("https://astia.narc.fi/uusiastia/aineisto/read.php", tietoQuery);
			string json = string.Empty;
			if (response.IsSuccessStatusCode)
			{
				json = await response.Content.ReadAsStringAsync();
			}
			else
			{
				string error = await response.Content.ReadAsStringAsync();
				throw new Exception(error);

			}

			dynamic aineistoResult = JsonObject.Parse(json);
			JsonArray tulokset = aineistoResult["tulokset"];
			dynamic aineisto = tulokset[0];
			dynamic id = aineisto["id"];
			return (string)id;
		}

		private async Task<JaksoAndTunniste> GetJaksoAndTunniste(string kuid)
		{
			var response = await http.GetAsync($"http://digi.narc.fi/fetchJaksoAndTunniste.php?kuid={kuid}");

			JaksoAndTunniste jaksoAndTunniste = new();
			if (response.IsSuccessStatusCode)
			{
				jaksoAndTunniste = await response.Content.ReadFromJsonAsync<JaksoAndTunniste>() ?? new JaksoAndTunniste();
			}
			else
			{
				string error = await response.Content.ReadAsStringAsync();
				throw new Exception(error);
			}

			return jaksoAndTunniste;
		}
	}
}