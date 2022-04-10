namespace AstiaClient
{
	using System.Threading.Tasks;
	using Astia;

	public class Program
	{
		public static async Task Main(string[] args)
		{
			//var oldUrl = args[0];
			var oldUrl = "http://digi.narc.fi/digi/view.ka?kuid=7137029";

			var astia = new Astia();

			var newUrl = await astia.GetNewUrl(oldUrl);
		}
	}
}