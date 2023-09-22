using System;
using System.Collections.Generic;

namespace locationsApp.Models.Responses.LocationResponse
{
	public class AddressComponent
	{
        public string shortName { get; set; }
        public string longName { get; set; }
        public List<int> types { get; set; }
    }
}

