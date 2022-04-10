namespace AstiaClient
{
	using System.Threading.Tasks;
	using Astia;

	public class Program
	{
		public static async Task Main(string[] args)
		{
			var oldUrl = args[0];
			var astia = new Astia();
			var newUrl = await astia.GetNewUrl(oldUrl);
			Console.WriteLine(newUrl);
		}
	}
}