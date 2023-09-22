using System;
using locationsApp.Models.Requests;
using locationsApp.Models.Responses;
using System.Threading.Tasks;
using locationsApp.Services.Interfaces;
using locationsApp.Constants;
using locationsApp.Models.Responses.LocationResponse;
using locationsApp.Models.Responses.HttpServiceResponse;
using System.Collections.Generic;
using Xamarin.Essentials;
using Newtonsoft.Json;

namespace locationsApp.Services.Implementations
{
    public class LocationService : ILocationService
    {
        #region Private Fields

        private readonly IHttpService httpService;
        private string baseUri { get; set; }

        #endregion

        #region Constructor

        public LocationService
        (
            IHttpService httpService
        )
        {
            this.httpService = httpService;
#if DEBUG
            baseUri = ServiceConstants.BaseStagingUri;
#else
            baseUri =  ServiceConstants.BaseProductionUri;
#endif
        }
        #endregion


        #region Public Methods
        public async Task<IList<SearchResponse>> SearchAsync(SearchRequest SearchCriteria)
        {
            try
            {
                var uri = $"{baseUri}{ServiceConstants.LocationsService}{ServiceConstants.Places}";
                var apikey = JsonConvert.DeserializeObject<LoginResponse>(Preferences.Get(SharedPreferencesKeys.ApiKey, SharedPreferencesKeys.Getter));

                var response = await httpService.GetAsync<HttpResponse<IList<SearchResponse>>>(uri, SearchCriteria, apikey.access_token);

                return response.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<LocationResponse> GetLocation(LocationRequest location)
        {
            try
            {
                var uri = $"{baseUri}{ServiceConstants.LocationsService}{ServiceConstants.Places}/{location.Id}";
                var apikey = JsonConvert.DeserializeObject<LoginResponse>(Preferences.Get(SharedPreferencesKeys.ApiKey, SharedPreferencesKeys.Getter));

                var response = await httpService.GetAsync<HttpResponse<LocationResponse>>(uri, null, apikey.access_token);

                return response.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<IList<PhotoResponse>> GetPhotos(IList<PhotoRequest> photos)
        {
            try
            {
                IList<PhotoResponse> returnedPhotos = new List<PhotoResponse>();

                foreach (var photo in photos)
                {
                    var uri = $"{baseUri}{ServiceConstants.LocationsService}{ServiceConstants.Photo}/{photo.Id}";
                    var apikey = JsonConvert.DeserializeObject<LoginResponse>(Preferences.Get(SharedPreferencesKeys.ApiKey, SharedPreferencesKeys.Getter));
                    var response = await httpService.GetAsync<HttpResponse<PhotoResponse>>(uri, null, apikey.access_token);

                    returnedPhotos.Add(response?.Data);
                }

                return returnedPhotos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion
    }
}

