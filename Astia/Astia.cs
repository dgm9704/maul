namespace Astia
{
	using System;
	using System.Net.Http;
	using System.Net.Http.Json;
	using System.Text.Json;
	using System.Text.Json.Nodes;
	using System.Threading.Tasks;

	public class Astia
	{
		private Lazy<HttpClient> http => new();

		public async Task<string> GetNewUrl(string oldUrl)
		{
			var kuid = ParseKuid(oldUrl);
			var jaksoAndTunniste = await GetJaksoAndTunniste(kuid);
			var aineistoId = await GetAineistoId(jaksoAndTunniste);
			if (!string.IsNullOrEmpty(aineistoId))
			{
				var tiedostoId = await GetTiedosto(aineistoId, jaksoAndTunniste.jakso);
				var newUrl = GetNewUrl(tiedostoId, aineistoId);
				return newUrl;
			}
			else
			{
				Console.WriteLine($"aineistoId failed for jaksoAndtunniste {jaksoAndTunniste}");
				return string.Empty;
			}
		}

		public static string ParseKuid(string url)
		=> url.Split('=').Last();

		public async Task<JaksoAndTunniste> GetJaksoAndTunniste(string kuid)
		{
			var uri = new Uri($"http://digi.narc.fi/fetchJaksoAndTunniste.php?kuid={kuid}");
			var json = await GetAsync(uri);
			return ParseJaksoAndTunniste(json);
		}

		public static JaksoAndTunniste ParseJaksoAndTunniste(string json)
		=> JsonSerializer.Deserialize<JaksoAndTunniste>(json);

		public async Task<string> GetAineistoId(JaksoAndTunniste jaksoAndTunniste)
		{
			TietoQuery tietoQuery = new() { searchString = $"AY_{jaksoAndTunniste.at3_ay_tunnus}", searchTarget = "aineisto" };
			var uri = new Uri("https://astia.narc.fi/uusiastia/aineisto/read.php");
			var json = await PostAsync(uri, tietoQuery);
			File.WriteAllText("aineistoresult.json", json);
			return ParseAineistoResult(json);
		}

		public static string ParseAineistoResult(string json)
		{
			dynamic aineistoResult = JsonObject.Parse(json) ?? new JsonObject();
			JsonArray tulokset = aineistoResult["tulokset"];
			if (tulokset != null)
			{
				dynamic aineisto = tulokset[0] ?? new JsonArray();
				dynamic id = aineisto["id"];
				return (string)id;
			}
			else
			{
				Console.WriteLine($"Tulokset is null");
				return string.Empty;
			}
		}


		public async Task<string> GetTiedosto(string id, string jakso)
		{
			var uri = new Uri($"https://astia.narc.fi/uusiastia/json/json_tiedostot.php?id={id}");
			var json = await GetAsync(uri);
			return ParseTiedostoResult(json, jakso);
		}

		public static string ParseTiedostoResult(string json, string jakso)
		{
			dynamic tiedostoResult = JsonObject.Parse(json) ?? new JsonObject();
			JsonArray tiedostot = tiedostoResult["fullres"];
			if (tiedostot != null)
			{
				foreach (var tiedosto in tiedostot)
				{
					var children = tiedosto?["children"];
					var secondChild = children?[1];
					if (Convert.ToString(secondChild?["tagData"]) == $"Tiedosto {jakso}")
					{
						var firstChild = children?[0];
						var result = Convert.ToString(firstChild?["tagData"]);
						return result ?? "";
					}
				}
			}
			else
			{
				Console.WriteLine($"Tiedostot is null, jakso: {jakso}");
			}
			return "";
		}

		public static string GetNewUrl(string tiedostoId, string aineistoId)
		=> $"https://astia.narc.fi/uusiastia/viewer/?fileId={tiedostoId}&aineistoId={aineistoId}";

		public async Task<string> GetAsync(Uri uri)
		{
			var response = await http.Value.GetAsync(uri);
			return await GetResponseContent(response);
		}

		public async Task<string> PostAsync(Uri uri, object data)
		{
			var response = await http.Value.PostAsJsonAsync(uri, data);
			return await GetResponseContent(response);
		}

		public async Task<string> GetResponseContent(HttpResponseMessage response)
		{
			string json = await response.Content.ReadAsStringAsync();
			return response.IsSuccessStatusCode
				? json
				: throw new Exception(json);
		}
	}


}