using System;
using System.Collections.Generic;
using System.Text;

namespace locationsApp.Models.Requests
{
    public class LoginRequest
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string grant_type { get; set; }
    }
}
