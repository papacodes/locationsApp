using locationsApp.Models.Requests;
using Xamarin.Forms;
using Prism.Navigation;
using locationsApp.Services.Interfaces;
using Prism.Commands;
using locationsApp.Extensions;
using locationsApp.Constants;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace locationsApp.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        #region public properties
        public LoginRequest LoginRequest { get; set; }
        public Color Color { get; set; }
        public string ErrorMessage { get; set; }
        public string LoaderDisplay { get; set; }
        #endregion

        #region private properties
        private readonly IAuthenticationService authenticationService;
        #endregion

        #region Constructor
        public LoginPageViewModel
        (
            IAuthenticationService authenticationService,
            INavigationService navigationService
        )
            :base (navigationService)
        {
            this.authenticationService = authenticationService;

            LoginRequest = new LoginRequest();

            //default values;
            LoginRequest.client_id = ServiceConstants.AuthenticationClientId;
            LoginRequest.client_secret = ServiceConstants.AuthenticationClientSecret;
            LoginRequest.grant_type = ServiceConstants.AuthenticationGrantType;
        }
        #endregion

        #region Commands
        public DelegateCommand LoginCommand
        {
            get
            {
                return new DelegateCommand
                (
                    async () =>
                    {
                        if (ValidateUserInput())
                        {
                            ExecuteLoader();

                            var response = await authenticationService.AuthenticateAsync(LoginRequest);

                            if (response.IsNotNull() )
                            {
                                Preferences.Set(SharedPreferencesKeys.ApiKey, JsonConvert.SerializeObject(response));
                                //Preferences.Set(SharedPreferencesKeys.UserDataKey, JsonConvert.SerializeObject(response.user));

                                await NavigationService.NavigateAsync($"{NavigationPaths.SearchLocationPage}");
                            }
                            else
                            {
                                Color = Color.Red;

                                ErrorMessage = AlertDialogConstants.InvalidLoginAlert;

                                PropertyToChange.Add(nameof(ErrorMessage));
                                PropertyToChange.Add(nameof(Color));

                                ExecutePropertyChange();
                            }

                        }
                        else
                        {
                            Color = Color.Red;

                            ErrorMessage = AlertDialogConstants.InvalidLoginAlert;

                            PropertyToChange.Add(nameof(ErrorMessage));
                            PropertyToChange.Add(nameof(Color));

                            ExecutePropertyChange();
                        }
                    }
                );
            }
            
        }
        #endregion

        #region private methdos
        private bool ValidateUserInput()
        {
            if (LoginRequest.client_id.IsNull() || LoginRequest.client_secret.IsNull())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region Prism Methods
        public override void Initialize(INavigationParameters parameters)
        {

        }
        #endregion
    }
}
