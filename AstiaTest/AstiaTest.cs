namespace AstiaTest
{
	using Xunit;
	using Astia;
	using System.IO;

	public class AstiaTest
	{
		[Fact]
		public void TestParseJaksoAndTunniste()
		{
			var expected = new JaksoAndTunniste { at3_ay_tunnus = "1294995.KA", ayid = "114889", jakso = "102" };
			var json = File.ReadAllText("jaksoAndTunnisteResult.json");
			var actual = Astia.ParseJaksoAndTunniste(json);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void TestParseAineistoId()
		{
			var expected = "1193635722";
			var json = File.ReadAllText("aineistoResult.json");
			var actual = Astia.ParseAineistoResult(json);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void TestParseTiedostResult()
		{
			var jakso = "102";
			var expected = "5829336113";
			var json = File.ReadAllText("tiedostoResult.json");
			var actual = Astia.ParseTiedostoResult(json, jakso);

			Assert.Equal(expected, actual);
		}
	}

}