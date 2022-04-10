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
		public void TestParseAineistoId()
		{
			var astia = new Astia();
			var jaksoAndTunniste = new JaksoAndTunniste { at3_ay_tunnus = "1294995.KA", ayid = "114889", jakso = "102" };
			var expected = "1193635722";
			var json = File.ReadAllText("aineistoResult.json");
			var actual = astia.ParseAineistoResult(json);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void TestParseTiedostResult()
		{
			var astia = new Astia();
			var expected = "5829336113";
			var json = File.ReadAllText("tiedostoResult.json");
			var actual = astia.ParseTiedostoResult(json);

			Assert.Equal(expected, actual);
		}
	}

}