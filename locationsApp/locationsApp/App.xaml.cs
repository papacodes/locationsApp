using System;
using locationsApp.Constants;
using locationsApp.Extensions;
using locationsApp.Services.Implementations;
using locationsApp.Services.Interfaces;
using locationsApp.ViewModels;
using locationsApp.Views;
using Newtonsoft.Json;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace locationsApp
{
    public partial class App : PrismApplication
    {

        public App() :
           this(null)
        { }

        public App(IPlatformInitializer initializer) :
           base(initializer)
        { }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //page registration
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<SearchLocationPage, SearchLocationPageViewModel>();
            containerRegistry.RegisterForNavigation<LocationPage, LocationPageViewModel>();

            //service registration
            containerRegistry.Register<IHttpService, HttpService>();
            containerRegistry.Register<IAuthenticationService, AuthenticationService>();
            containerRegistry.Register<ILocationService, LocationService>();

        }

        // Called when the PrismApplication has completed it's initialization process.
        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync($"/{NavigationPaths.NavigationPage}{NavigationPaths.LoginPage}");
            
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

