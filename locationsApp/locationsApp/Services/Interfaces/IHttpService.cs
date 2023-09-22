using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace locationsApp.Services.Interfaces
{
    public interface IHttpService
    {
        Task<T> GetAsync<T>(string url, object request = null, string apikey = null, JsonSerializerSettings jsonSerializerSettings = null);

        Task<T> PostAsync<T, U>(string url, object request, string apikey = null, JsonSerializerSettings jsonSerializerSettings = null);

        Task<T> PostAsync<T>(string url, object request, string apikey = null, JsonSerializerSettings jsonSerializerSettings = null);

        Task<T> PostDocumentAsync<T>(string url, object request, byte[] image, string apikey = null, JsonSerializerSettings jsonSerializerSettings = null);

        Task<T> PostLongUriAsync<T>(string url, object request, string apikey = null, JsonSerializerSettings jsonSerializerSettings = null);

        Task<T> PutAsync<T>(string url, object request, string apikey = null, JsonSerializerSettings jsonSerializerSettings = null);

        Task<T> PutLongUriAsync<T>(string url, object request, string apikey = null, JsonSerializerSettings jsonSerializerSettings = null);

        Task<T> DeleteAsync<T>(string url, object request = null, string apikey = null, JsonSerializerSettings jsonSerializerSettings = null);
    }
}
