namespace Astia
{
	using System;
	using System.Dynamic;
	using System.Net.Http;
	using System.Net.Http.Json;
	using System.Text.Json;
	using System.Text.Json.Nodes;
	using System.Threading.Tasks;

	public class Astia
	{
		private HttpClient http = new();

		public async Task<string> GetNewUrl(string oldUrl)
		{

			//var kuid = oldUrl.Split('=').Last();
			var kuid = "7137029";

			//var jaksoAndtunniste = await GetJaksoAndTunniste(kuid);
			var jaksoAndtunniste = new JaksoAndTunniste { at3_ay_tunnus = "1294995.KA", ayid = 114889, jakso = 102 };

			//var id = await GetAineistoId(jaksoAndtunniste);
			var id = "1193635722";

			string tiedosto = await GetTiedosto(jaksoAndtunniste, id);

			return oldUrl;
		}

		public async Task<JaksoAndTunniste> GetJaksoAndTunniste(string kuid)
		{
			var uri = new Uri($"http://digi.narc.fi/fetchJaksoAndTunniste.php?kuid={kuid}");
			var json = await GetAsync(uri);
			return await ParseJaksoAndTunniste(json);
		}

		public async Task<string> GetAineistoId(JaksoAndTunniste jaksoAndTunniste)
		{
			TietoQuery tietoQuery = new() { searchString = $"AY_{jaksoAndTunniste.at3_ay_tunnus}", searchTarget = "aineisto" };
			var uri = new Uri("https://astia.narc.fi/uusiastia/aineisto/read.php");
			var json = await PostAsync(uri, tietoQuery);
			return await ParseAineistoResult(json);
		}

		public async Task<JaksoAndTunniste> ParseJaksoAndTunniste(string json)
		=> JsonSerializer.Deserialize<JaksoAndTunniste>(json);

		public async Task<string> ParseAineistoResult(string json)
		{
			dynamic aineistoResult = JsonObject.Parse(json);
			JsonArray tulokset = aineistoResult["tulokset"];
			dynamic aineisto = tulokset[0];
			dynamic id = aineisto["id"];
			return (string)id;
		}

		public async Task<string> GetTiedosto(JaksoAndTunniste jaksoAndtunniste, string id)
		{
			var response = await http.GetAsync($"https://astia.narc.fi/uusiastia/json/json_tiedostot.php?id={id}");
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

			return ParseTiedostoResult(json);

		}

		public string ParseTiedostoResult(string json)
		{
			throw new NotImplementedException();
		}

		public async Task<string> GetAsync(Uri uri)
		{
			var response = await http.GetAsync(uri);
			return await GetResponseContent(response);
		}

		public async Task<string> PostAsync(Uri uri, object data)
		{
			var response = await http.PostAsJsonAsync(uri, data);
			return await GetResponseContent(response);
		}

		public async Task<string> GetResponseContent(HttpResponseMessage response)
		{
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
			return json;
		}
	}


}