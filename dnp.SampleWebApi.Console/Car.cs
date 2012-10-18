using System;

namespace dnp.SampleWebApi.Console
{
	class Car
	{
		public Guid ID { get; set; }
		public string Kategorie { get; set; }
		public string Typ { get; set; }
		public string Kennzeichen { get; set; }
		public int Kilometerstand { get; set; }
		public DateTime NächsteUntersuchung { get; set; }
	}
}