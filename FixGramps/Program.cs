using System.Xml.Linq;

namespace FixGramps
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			var inputFile = args[0];
			var outputFile = inputFile.Replace(".gramps", "_out.gramps");
			var doc = XDocument.Load(inputFile);
			var modified = AddAstiaLinks(doc);
			modified.Save(outputFile);
		}
		public static XDocument AddAstiaLinks(XDocument document)
		{
			var gramps = document.Root?.GetDefaultNamespace()
				?? "http://gramps-project.org/xml/1.7.1/";

			var astia = new Astia.Astia();
			var notes =
				document.
				Descendants(gramps + "note").
				Where(n => n.Attribute("type")?.Value.ToLower() == "link" || n.Attribute("type")?.Value.ToLower() == "url").
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
				if (!string.IsNullOrEmpty(newUrl))
				{
					text.Value += Environment.NewLine;
					var start = text.Value.Length;
					text.Value += newUrl;
					var end = text.Value.Length;

					var style = new XElement(
						gramps + "style",
						new XAttribute("name", "link"),
						new XAttribute("value", newUrl),
						new XElement(gramps + "range",
							new XAttribute("start", start),
							new XAttribute("end", end)));

					note.Add(style);

					Console.WriteLine($"{oldUrl} -> {newUrl}");
				}
				else
				{
					Console.WriteLine($"failed for {oldUrl}");
				}
			}

			return document;
		}
	}
}