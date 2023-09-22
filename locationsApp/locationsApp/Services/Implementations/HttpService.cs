using Newtonsoft.Json;
using locationsApp.Constants;
using locationsApp.Extensions;
using locationsApp.Services.Exceptions;
using locationsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace locationsApp.Services.Implementations
{
    public class HttpService : IHttpService
    {

        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public HttpService()
        {
            _jsonSerializerSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatString = "dd/MM/yyyy HH:mm:ss"
            };

        }


        public async Task<T> GetAsync<T>(string url, object request = null, string apikey = null, JsonSerializerSettings jsonSerializerSettings = null)
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apikey);

                Uri queryUrl = new Uri(url);

                if (request.IsNotNull())
                {
                    var queryStr = await GetQueryStringAsync(request);
                    queryUrl = new Uri(url + "?" + queryStr);
                }

                var response = await httpClient.GetAsync(queryUrl);

                var stream = await response.Content.ReadAsStringAsync();
                stream = stream.Replace("[]", "null");

                httpClient.DefaultRequestHeaders.Clear();

                if (jsonSerializerSettings is null)
                {
                    jsonSerializerSettings = _jsonSerializerSettings;
                }


                if (!response.IsSuccessStatusCode)
                {
                    var exception = new HttpServiceException(response.StatusCode, response.Content, null, response.ReasonPhrase, JsonConvert.DeserializeObject<Models.Responses.HttpServiceResponse.HttpResponse<string>>(stream, jsonSerializerSettings), url);

                    throw exception;
                }

                return JsonConvert.DeserializeObject<T>(stream);

            }
            catch (UnauthorizedAccessException uae)
            {
                throw uae;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<T> PostAsync<T>(string url, object request, string apikey = null, JsonSerializerSettings jsonSerializerSettings = null)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apikey);
                    httpClient.DefaultRequestHeaders.Add(ServiceConstants.RequestHeaderDefault, ServiceConstants.RequestHeaderDefaultValue);

                    Uri queryUrl = new Uri(url);
                    var json = JsonConvert.SerializeObject(request);
                    var keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                    var content = GenerateFormUrlEncoded(keyValuePairs);
                    var stringContent = new StringContent(content, Encoding.UTF8, "application/x-www-form-urlencoded");
                    var response = await httpClient.PostAsync(queryUrl, stringContent);

                    var stream = await response.Content.ReadAsStringAsync();
                    stream = stream.Replace("[]", "null");

                    httpClient.DefaultRequestHeaders.Clear();

                    if (jsonSerializerSettings is null)
                    {
                        jsonSerializerSettings = _jsonSerializerSettings;
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        var exception = new HttpServiceException(response.StatusCode, response.Content, null, response.ReasonPhrase, JsonConvert.DeserializeObject<Models.Responses.HttpServiceResponse.HttpResponse<string>>(stream, jsonSerializerSettings), url);

                        throw exception;
                    }

                    return JsonConvert.DeserializeObject<T>(stream, jsonSerializerSettings);
                }
            }
            catch (UnauthorizedAccessException uae)
            {
                throw uae;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<T> PostDocumentAsync<T>(string url, object request, byte[] image, string apikey = null, JsonSerializerSettings jsonSerializerSettings = null)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("API-KEY", apikey);

                    Uri queryUrl = new Uri(url);
                    StringContent json = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                    var form = new MultipartFormDataContent();
                    form.Add(new ByteArrayContent(new MemoryStream(image).ToArray()), "FileContent", "genericphoto.jpg");

                    var response = await httpClient.PostAsync(queryUrl, json);

                    var stream = await response.Content.ReadAsStringAsync();
                    stream = stream.Replace("[]", "null");

                    httpClient.DefaultRequestHeaders.Clear();

                    if (jsonSerializerSettings is null)
                    {
                        jsonSerializerSettings = _jsonSerializerSettings;
                    }

                    if (!response.IsSuccessStatusCode)
                    {
                        var exception = new HttpServiceException(response.StatusCode, response.Content, null, response.ReasonPhrase, JsonConvert.DeserializeObject<Models.Responses.HttpServiceResponse.HttpResponse<string>>(stream, jsonSerializerSettings), url);

                        throw exception;
                    }

                    return JsonConvert.DeserializeObject<T>(stream, jsonSerializerSettings);
                }
            }
            catch (UnauthorizedAccessException uae)
            {
                throw uae;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<T> PostAsync<T, U>(string url, object request, string apikey = "", JsonSerializerSettings jsonSerializerSettings = null)
        {
            try
            {
                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Add("apikey", apikey);

                Uri queryUrl = new Uri(url);
                var json = JsonConvert.SerializeObject(request);
                var keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                var stringContent = new FormUrlEncodedContent(keyValuePairs);

                var response = await httpClient.PostAsync(queryUrl, stringContent);

                var stream = await response.Content.ReadAsStringAsync();
                stream = stream.Replace("[]", "null");

                httpClient.DefaultRequestHeaders.Clear();

                if (jsonSerializerSettings == null)
                    jsonSerializerSettings = _jsonSerializerSettings;

                if (!response.IsSuccessStatusCode)
                {
                    var exception = new HttpServiceException<U>(response.StatusCode, response.Content, null, response.ReasonPhrase, JsonConvert.DeserializeObject<Models.Responses.HttpServiceResponse.HttpResponse<U>>(stream, jsonSerializerSettings), url);

                    throw exception;
                }

                return JsonConvert.DeserializeObject<T>(stream, jsonSerializerSettings);
            }
            catch (UnauthorizedAccessException uae)
            {
                throw uae;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




        public async Task<T> PostLongUriAsync<T>(string url, object request, string apikey = null, JsonSerializerSettings jsonSerializerSettings = null)
        {
            try
            {
                var httpClient = new HttpClient();

                var json = JsonConvert.SerializeObject(request);

                httpClient.DefaultRequestHeaders.Add("apikey", apikey);

                Uri queryUrl = new Uri(url);
                var keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                var content = GenerateFormUrlEncoded(keyValuePairs);
                var stringContent = new StringContent(content, Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await httpClient.PostAsync(queryUrl, stringContent);

                var stream = await response.Content.ReadAsStringAsync();
                stream = stream.Replace("[]", "null");

                httpClient.DefaultRequestHeaders.Clear();

                if (jsonSerializerSettings is null)
                {
                    jsonSerializerSettings = _jsonSerializerSettings;
                }

                if (!response.IsSuccessStatusCode)
                {
                    var exception = new HttpServiceException(response.StatusCode, response.Content, null, response.ReasonPhrase, JsonConvert.DeserializeObject<Models.Responses.HttpServiceResponse.HttpResponse<string>>(stream, jsonSerializerSettings), url);

                    throw exception;
                }

                return JsonConvert.DeserializeObject<T>(stream, jsonSerializerSettings);
            }
            catch (UnauthorizedAccessException uae)
            {
                throw uae;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<T> PutAsync<T>(string url, object request, string apikey = null, JsonSerializerSettings jsonSerializerSettings = null)
        {
            try
            {
                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Add("apikey", apikey);

                Uri queryUrl = new Uri(url);
                var json = JsonConvert.SerializeObject(request);
                var keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                var stringContent = new FormUrlEncodedContent(keyValuePairs);

                var response = await httpClient.PutAsync(queryUrl, stringContent);

                var stream = await response.Content.ReadAsStringAsync();
                stream = stream.Replace("[]", "null");

                httpClient.DefaultRequestHeaders.Clear();

                if (jsonSerializerSettings is null)
                {
                    jsonSerializerSettings = _jsonSerializerSettings;
                }

                if (!response.IsSuccessStatusCode)
                {
                    var exception = new HttpServiceException(response.StatusCode, response.Content, null, response.ReasonPhrase, JsonConvert.DeserializeObject<Models.Responses.HttpServiceResponse.HttpResponse<string>>(stream, jsonSerializerSettings), url);

                    throw exception;
                }

                return JsonConvert.DeserializeObject<T>(stream, jsonSerializerSettings);
            }
            catch (UnauthorizedAccessException uae)
            {
                throw uae;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<T> PutLongUriAsync<T>(string url, object request, string apikey = null, JsonSerializerSettings jsonSerializerSettings = null)
        {
            try
            {
                var httpClient = new HttpClient();

                var json = JsonConvert.SerializeObject(request);

                httpClient.DefaultRequestHeaders.Add("apikey", apikey);

                Uri queryUrl = new Uri(url);
                var keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                var content = GenerateFormUrlEncoded(keyValuePairs);
                var stringContent = new StringContent(content, Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await httpClient.PutAsync(queryUrl, stringContent);

                var stream = await response.Content.ReadAsStringAsync();
                stream = stream.Replace("[]", "null");

                httpClient.DefaultRequestHeaders.Clear();

                if (jsonSerializerSettings is null)
                {
                    jsonSerializerSettings = _jsonSerializerSettings;
                }

                if (!response.IsSuccessStatusCode)
                {
                    var exception = new HttpServiceException(response.StatusCode, response.Content, null, response.ReasonPhrase, JsonConvert.DeserializeObject<Models.Responses.HttpServiceResponse.HttpResponse<string>>(stream, jsonSerializerSettings), url);

                    throw exception;
                }

                return JsonConvert.DeserializeObject<T>(stream, jsonSerializerSettings);
            }
            catch (UnauthorizedAccessException uae)
            {
                throw uae;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<T> DeleteAsync<T>(string url, object request = null, string apikey = null, JsonSerializerSettings jsonSerializerSettings = null)
        {
            try
            {
                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Add("apikey", apikey);

                Uri queryUrl = new Uri(url);

                var response = await httpClient.DeleteAsync(queryUrl);

                var stream = await response.Content.ReadAsStringAsync();
                stream = stream.Replace("[]", "null");

                httpClient.DefaultRequestHeaders.Clear();

                if (jsonSerializerSettings is null)
                {
                    jsonSerializerSettings = _jsonSerializerSettings;
                }

                if (!response.IsSuccessStatusCode)
                {
                    var exception = new HttpServiceException(response.StatusCode, response.Content, null, response.ReasonPhrase, JsonConvert.DeserializeObject<Models.Responses.HttpServiceResponse.HttpResponse<string>>(stream, jsonSerializerSettings), url);

                    throw exception;
                }

                return JsonConvert.DeserializeObject<T>(stream, jsonSerializerSettings);
            }
            catch (UnauthorizedAccessException uae)
            {
                throw uae;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async Task<string> GetQueryStringAsync(object request)
        {
            var serialisedJson = JsonConvert.SerializeObject(request);
            var keyValuePairDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(serialisedJson);
            var formUrlEncodedData = new FormUrlEncodedContent(keyValuePairDictionary);

            return await formUrlEncodedData.ReadAsStringAsync();
        }

        private Dictionary<string, string> GetKeyValuePairDictionary(object request, string url)
        {
            var serialisedJson = JsonConvert.SerializeObject(request);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(serialisedJson);
            dictionary.Add("Url", url);
            return dictionary;
        }

        private string GenerateFormUrlEncoded(Dictionary<string, string> content)
        {
            var result = string.Empty;

            foreach (var item in content)
            {
                result += $"{item.Key}={item.Value}&";
            }

            return result;
        }
    }

}

