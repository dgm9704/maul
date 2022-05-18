// Generated by https://quicktype.io
//
// To change quicktype's target language, run command:
//
//   "Set quicktype target language"

namespace QuickType
{
	using System;
	using System.Collections.Generic;

	using System.Globalization;
	using System.Text.Json;

	public partial class AineistoResult
	{
		public long lkm { get; set; }
		public Dictionary<string, HakuValue> haku { get; set; }
		public Fasetit fasetit { get; set; }
		public Tulokset[] tulokset { get; set; }
		public long status { get; set; }
	}

	public partial class Fasetit
	{
		public object[] rcFacets { get; set; }
		public object[] locFacets { get; set; }
		public ClassFacets1[] classFacets1 { get; set; }
		public ClassFacets1[] classFacets2 { get; set; }
		public object[] aineistoFacets { get; set; }
		public object[] placeFacets { get; set; }
		public object[] typeFacets { get; set; }
	}

	public partial class ClassFacets1
	{
		public ClassFacets1[] children { get; set; }
	}

	public partial class Tulokset
	{
		public string type { get; set; }
		public long id { get; set; }
		public Ajat[] ajat { get; set; }
		public string titles { get; set; }
		public Ajat[] tunnisteet { get; set; }
		public string level { get; set; }
		public string tarkenne { get; set; }
		public string sisaltyvat { get; set; }
		public Ajat[] ylemmat { get; set; }
		public Rajoitukset[] rajoitukset { get; set; }
		public string tietosisallot { get; set; }
		public Ajat[] asiasanat { get; set; }
		public Ajat[] relaatiot { get; set; }
		public Ajat[] ilmentymat { get; set; }
		public string paikat { get; set; }
		public string aineistotyyppi { get; set; }
		public Rajoitukset[] seuraamukset { get; set; }
		public string jarjestamisperiaate { get; set; }
		public string laajuus { get; set; }
		public string apuaineistot { get; set; }
		public object kunto { get; set; }
		public bool nayttorajoitus { get; set; }
		public Rajoitukset[] rajoitusperusteet { get; set; }
		public Ajat[] kayttoluvanMyontaja { get; set; }
		public object hakemisto { get; set; }
	}

	public partial class Ajat
	{
		public string name { get; set; }
		public object[] attrs { get; set; }
		public Rajoitukset[] children { get; set; }
	}

	public partial class Rajoitukset
	{
		public string name { get; set; }
		public object[] attrs { get; set; }
		public string tagData { get; set; }
	}

	public enum HakuEnum { Aineisto, Ay1294995Ka, Empty };

	public partial struct HakuValue
	{
		public HakuEnum? Enum;
		public long? Integer;

		public static implicit operator HakuValue(HakuEnum Enum) => new HakuValue { Enum = Enum };
		public static implicit operator HakuValue(long Integer) => new HakuValue { Integer = Integer };
	}
}
