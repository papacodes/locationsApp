using System;
using System.Collections.Generic;

namespace locationsApp.Models.Responses.LocationResponse
{
	public class LocationResponse
	{
        public List<AddressComponent> addressComponents { get; set; }
        public string formattedAddress { get; set; }
        public string url { get; set; }
        public int utcOffset { get; set; }
        public string vicinity { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string name { get; set; }
        public List<Photo> photos { get; set; }
        public string placeId { get; set; }
        public List<int> types { get; set; }
    }
}

