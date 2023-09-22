using System;
using System.Collections.Generic;

namespace locationsApp.Models.Responses.LocationResponse
{
	public class SearchResponse
	{
        public string placeId { get; set; }
        public string description { get; set; }
        public string mainText { get; set; }
        public string secondaryText { get; set; }
        public List<Term> terms { get; set; }
        public List<int> types { get; set; }

    }
}

