using locationsApp.Models.Responses.HttpServiceResponse;
using System;
using System.Net;
using System.Net.Http;

namespace locationsApp.Services.Exceptions
{
    public class HttpServiceException<T> : Exception
    {
        public string ContentString { get; set; }
        public string UrlString { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public HttpContent Content { get; set; }
        public string ErrorMessage { get; set; }
        public string HttpResponseMessage { get; set; }

        public HttpServiceException(HttpStatusCode statusCode, HttpContent content, string contentString, string message, HttpResponse<T> httpResponse = default(HttpResponse<T>), string urlString = null)
        {
            StatusCode = statusCode;
            ContentString = contentString;
            UrlString = urlString;
            Content = content;
            ErrorMessage = message;
        }
    }

    public class HttpServiceException : Exception
    {
        public string ContentString { get; set; }
        public string UrlString { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public HttpContent Content { get; set; }
        public string ErrorMessage { get; set; }
        public string HttpResponseMessage { get; set; }

        public HttpServiceException(HttpStatusCode statusCode, HttpContent content, string contentString, string message, HttpResponse<string> httpResponse = default(HttpResponse<string>), string urlString = null)
        {
            StatusCode = statusCode;
            ContentString = contentString;
            UrlString = urlString;
            Content = content;
            ErrorMessage = message;
        }
    }
}
