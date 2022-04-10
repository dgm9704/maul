namespace UusiaAstia
{
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
		public async Task GetNewUrl(string kuid)
		{
			HttpClient http = new();

			// var response = await http.GetAsync($"http://digi.narc.fi/fetchJaksoAndTunniste.php?kuid={kuid}");

			// JaksoAndTunniste jaksoAndTunniste = new();
			// if (response.IsSuccessStatusCode)
			// {
			// 	jaksoAndTunniste = await response.Content.ReadFromJsonAsync<JaksoAndTunniste>() ?? new JaksoAndTunniste();
			// 	//Console.WriteLine(jaksoAndTunniste?.ToString());
			// }
			// else
			// {
			// 	string error = await response.Content.ReadAsStringAsync();
			// 	Console.WriteLine(error);
			// 	return;
			// }

			// TietoQuery tietoQuery = new() { searchString = $"AY_{jaksoAndTunniste?.at3_ay_tunnus}", searchTarget = "aineisto" };

			// response = await http.PostAsJsonAsync("https://astia.narc.fi/uusiastia/aineisto/read.php", tietoQuery);
			// string json = string.Empty;
			// if (response.IsSuccessStatusCode)
			// {
			// 	//aineistoResult = await response.Content.ReadFromJsonAsync<ExpandoObject>() ?? new ExpandoObject();
			// 	json = await response.Content.ReadAsStringAsync();
			// 	//File.WriteAllText("aineistoResult.json", json);


			// }
			// else
			// {
			// 	string result = await response.Content.ReadAsStringAsync();
			// 	Console.WriteLine(result);
			// 	return;
			// }

			// string json = File.ReadAllText("aineistoResult.json");

			// dynamic aineistoResult = JsonObject.Parse(json);
			// JsonArray tulokset = aineistoResult["tulokset"];
			// dynamic aineisto = tulokset[0];
			// dynamic id = aineisto["id"];

			// Console.WriteLine(id);
			string id = "1193635722";
			var response = await http.GetAsync($"https://astia.narc.fi/uusiastia/json/json_tiedostot.php?id={id}");
			string json = string.Empty;
			if (response.IsSuccessStatusCode)
			{
				var tiedostoResult = await response.Content.ReadFromJsonAsync<ExpandoObject>() ?? new ExpandoObject();
				json = await response.Content.ReadAsStringAsync();
				File.WriteAllText("tiedostoResult.json", json);
			}
			else
			{
				string result = await response.Content.ReadAsStringAsync();
				Console.WriteLine(result);
				return;
			}


		}
	}
}