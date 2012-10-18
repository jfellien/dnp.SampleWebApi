using System;
using System.Collections.Generic;
using System.Net;

namespace dnp.SampleWebApi.Console
{
	class CarWebApiResponse
	{
		public bool IsNotSuccessful { get; set; }
		public HttpStatusCode StatusCode { get; set; }
		public string Reason { get; set; }
		public Car Car { get; set; }
		public IEnumerable<Car> Cars { get; set; }
	}
}