using System;
namespace locationsApp.Models.Responses.LocationResponse
{
	public class Meta
	{
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalCount { get; set; }
        public int totalPages { get; set; }
        public string message { get; set; }
    }
}

