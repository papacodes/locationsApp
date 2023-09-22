using System;
using System.Collections.Generic;
using System.Text;
using locationsApp.Models.Responses.LocationResponse;

namespace locationsApp.Models.Responses.HttpServiceResponse
{
    public class HttpResponse<T>
    {
        public Meta meta { get; set; }
        public T Data { get; set; }
    }
}
