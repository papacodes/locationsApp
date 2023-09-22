using System;
using System.Collections.Generic;
using System.Text;

namespace locationsApp.Constants
{
    public class ServiceConstants
    {
        public const string BaseProductionUri = "";
        public const string BaseStagingUri = "https://staging.api.eos.kerridgecs.online/location/api/v1";

        public static string RequestHeaderDefault = "Accept";
        public static string RequestHeaderDefaultValue = "application/json";

        public static string AuthenticationClientId = "94eb850d-448f-48ef-a085-5fee4807606e.apps.kerridgecs.com";
        public static string AuthenticationClientSecret = "b609f130-2d13-43d4-93df-f8ab9f1cafde";
        public static string AuthenticationGrantType = "client_credentials";

        public const string AuthenticationService = "https://staging.identity.eos.kerridgecs.online/connect/token"; //seems to be on a different uris even the base uri seems to be differnt for authentication so we will deal with it differently
        public const string LoginService = "/login";
        public const string LocationsService = "/locations";
        public const string Places = "/places";
        public const string Photo = "/photos";
    }
}
