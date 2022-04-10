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

		// //var oldUrl = args[0];
		// var oldUrl = "http://digi.narc.fi/digi/view.ka?kuid=7137029";

		// var astia = new Astia();


		// //var kuid = oldUrl.Split('=').Last();
		// var kuid = "7137029";

		// //var jaksoAndtunniste = await GetJaksoAndTunniste(kuid);
		// var jaksoAndtunniste = new JaksoAndTunniste { at3_ay_tunnus = "1294995.KA", ayid = 114889, jakso = 102 };

		// //var id = await GetAineistoId(jaksoAndtunniste);
		// var id = "1193635722";

		// string tiedosto = await GetTiedosto(jaksoAndtunniste, id);

		// return oldUrl;

	}

}