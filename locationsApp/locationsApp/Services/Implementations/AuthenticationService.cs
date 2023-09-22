using System;
using System.Threading.Tasks;
using locationsApp.Constants;
using locationsApp.Extensions;
using locationsApp.Models.Requests;
using locationsApp.Models.Responses;
using locationsApp.Models.Responses.HttpServiceResponse;
using locationsApp.Services.Interfaces;

namespace locationsApp.Services.Implementations
{
	public class AuthenticationService : IAuthenticationService
    {
        #region Private Fields

        private readonly IHttpService httpService;
        private string baseUri { get; set; }

        #endregion

        #region public fields



        #endregion

        #region Constructor

        public AuthenticationService
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

        public async Task<LoginResponse> AuthenticateAsync(LoginRequest LoginCredentials)
        {
            try
            {
                var uri = $"{ServiceConstants.AuthenticationService}";

                var response = await httpService.PostAsync<LoginResponse>(uri, LoginCredentials);

                return response;
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