namespace AstiaTest
{
	using Xunit;
	using System.Threading.Tasks;
	using Astia;

	public class AstiaTest
	{
		[Fact]
		public async Task TestGetJaksoAndTunniste()
		{
			var astia = new Astia();
			var kuid = "7137029";
			var expected = new JaksoAndTunniste { at3_ay_tunnus = "1294995.KA", ayid = 114889, jakso = 102 };
			var actual = await astia.GetJaksoAndTunniste(kuid);

			Assert.Equal(expected, actual);
		}

		[Fact]
		public async Task TestGetAineistoId()
		{
			var astia = new Astia();
			var jaksoAndTunniste = new JaksoAndTunniste { at3_ay_tunnus = "1294995.KA", ayid = 114889, jakso = 102 };
			var expected = "1193635722";
			var actual = await astia.GetAineistoId(jaksoAndTunniste);

			Assert.Equal(expected, actual);
		}



		// string tiedosto = await GetTiedosto(jaksoAndtunniste, id);

		// return oldUrl;

	}

}