using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dnp.SampleWebApi.Models
{
	public class Car
	{
		public Guid ID { get; set; }
		public string Kategorie { get; set; }
		public string Typ { get; set; }
		public string Kennzeichen { get; set; }
		public int Kilometerstand { get; set; }
		public DateTime NächsteUntersuchung { get; set; }
	}
}