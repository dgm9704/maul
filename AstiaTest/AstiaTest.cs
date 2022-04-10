namespace AstiaTest
{
	using Xunit;
	using System.Threading.Tasks;
	using Astia;
	using System.IO;

	public class AstiaTest
	{
		[Fact]
		public void TestParseJaksoAndTunniste()
		{
			var astia = new Astia();
			var expected = new JaksoAndTunniste { at3_ay_tunnus = "1294995.KA", ayid = "114889", jakso = "102" };
			var json = File.ReadAllText("jaksoAndTunnisteResult.json");
			var actual = astia.ParseJaksoAndTunniste(json);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public async Task TestGetAineistoId()
		{
			var astia = new Astia();
			var jaksoAndTunniste = new JaksoAndTunniste { at3_ay_tunnus = "1294995.KA", ayid = "114889", jakso = "102" };
			var expected = "1193635722";
			var actual = await astia.GetAineistoId(jaksoAndTunniste);

			Assert.Equal(expected, actual);
		}



		// string tiedosto = await GetTiedosto(jaksoAndtunniste, id);

		// return oldUrl;

	}

}