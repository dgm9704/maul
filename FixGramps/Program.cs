using System.Xml.Linq;

namespace FixGramps
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var inputFile = args[0];
			var outputFile = Path.ChangeExtension(inputFile, "out");
			var doc = XDocument.Load(inputFile);
			var modified = AddAstiaLinks(doc);
			modified.Save(outputFile);
		}

		public static XNamespace gramps = "http://gramps-project.org/xml/1.7.1/";

		public static XDocument AddAstiaLinks(XDocument document)
		{
			var astia = new Astia.Astia();
			var notes =
				document.
				Descendants(gramps + "note").
				Where(n => n.Attribute("type")?.Value == "Link").
				Where(n =>
					n.
					Descendants(gramps + "text").
					First().
					Value.
					Contains("digi.narc.fi/digi/view.ka?kuid="));

			foreach (var note in notes)
			{
				var text = note.Descendants(gramps + "text").First();
				var oldUrl = text.Value;
				var newUrl = astia.GetNewUrl(oldUrl).Result;
				text.Value += Environment.NewLine + newUrl;
			}

			return document;
		}
	}
}