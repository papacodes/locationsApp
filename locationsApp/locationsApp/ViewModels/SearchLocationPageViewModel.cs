using locationsApp.Constants;
using locationsApp.Extensions;
using locationsApp.Models.Requests;
using locationsApp.Models.Responses.LocationResponse;
using locationsApp.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace locationsApp.ViewModels
{
    class SearchLocationPageViewModel : BaseViewModel
    {
        #region Private Properties
        private readonly ILocationService locationService;
        #endregion

        #region Public Properties
        //public Color Color { get; set; }
        public string Searech { get; set; }
        public string Animation { get; set; }
        public string ScannerMessage { get; set; }
        public string ButtonText { get; set; }
        public string AnimationFrequency { get; set; }
        public string ErrorMessage { get; set; }

        public bool SearchList { get; set; }

        public event EventHandler OnSearchResultEvent;

        public SearchRequest SearchRequest { get; set; }
        public IList<SearchResponse> SearchResponses { get; set; }
        #endregion

        #region Commands
        public DelegateCommand SearchCommand
        {
            get
            {
                return new DelegateCommand
                (
                    async () =>
                    {
                        if (SearchRequest.criteria.IsNotNull())
                        {
                            ExecuteLoader();

                            SearchResponses = await locationService.SearchAsync(SearchRequest);

                            ExecuteLoader();

                            if (SearchResponses.IsNotNull())
                            {
                                //navigate to the list page
                                PropertyToChange.Add(nameof(SearchResponses));
                                SearchList = true;
                                PropertyToChange.Add(nameof(SearchList));
                                ExecutePropertyChange();

                                OnSearchResultEvent?.Invoke(this, EventArgs.Empty);
                            }
                            else
                            {
                                ErrorViewUpdate();
                            }
                        }
                        else
                        {
                            ErrorViewUpdate();
                        }

                        

                    }
                );
            }
        }

        public DelegateCommand<SearchResponse> LocationSelector
        {
            get
            {
                return new DelegateCommand<SearchResponse>(async (location) =>
                {

                    LocationRequest locationRequest = new LocationRequest { Id = location.placeId };
                    ExecuteLoader();
                    var response = await locationService.GetLocation(locationRequest);

                    if (response.IsNotNull())
                    {
                        IList<PhotoRequest> photoRequest = response.photos.Select(photo => new PhotoRequest { Id = photo.photoId }).ToList();

                        var photos = await locationService.GetPhotos(photoRequest);
                        Preferences.Set(SharedPreferencesKeys.PhotosKey, JsonConvert.SerializeObject(photos));
                        Preferences.Set(SharedPreferencesKeys.LocationKey, JsonConvert.SerializeObject(response));
                        await NavigationService.NavigateAsync($"{NavigationPaths.LocationPage}");
                    }
                    else
                    {
                        ErrorViewUpdate();
                    }

                    ExecuteLoader();
                   
                });
            }
        }

        
        #endregion

        #region Constructor
        public SearchLocationPageViewModel
        (
            INavigationService navigationService,
            ILocationService locationService
        )
            :base(navigationService)
        {
            this.locationService = locationService;
            SearchList = false;
            SetPagePrecursors();
        }

        #endregion

        #region public methods
        public void SetPagePrecursors()
        {
            
        }


        public void SuccessfulScan()
        {
            ButtonText = "Location found.";
            Animation = AnimationConstants.Success;
            AnimationFrequency = AnimationConstants.RestartLoop;

            PropertyToChange.Add(nameof(ButtonText));

            AnimationPropertyChangeApply();
        }

        public void PleaseWaitViewUpdate()
        {
            Animation = AnimationConstants.Search;
            ScannerMessage = "Please Wait...";
            AnimationFrequency = AnimationConstants.InfiniteLoop;

            AnimationPropertyChangeApply();
        }

        public void ErrorViewUpdate()
        {
            Animation = AnimationConstants.Error;
            ScannerMessage = $"Search criteria /'{SearchRequest.criteria}/' not found";
            AnimationFrequency = AnimationConstants.RestartLoop;

            AnimationPropertyChangeApply();
        }

        public void AnimationPropertyChangeApply()
        {
            PropertyToChange.Add(nameof(ScannerMessage));
            PropertyToChange.Add(nameof(Animation));
            PropertyToChange.Add(nameof(AnimationFrequency));
            ExecutePropertyChange();
        }

        public void InputErrorViewUpdate()
        {
            //Color = Color.Red;

            ScannerMessage = AlertDialogConstants.InvalidLoginAlert;
            AnimationFrequency = AnimationConstants.RestartLoop;

            PropertyToChange.Add(nameof(ErrorMessage));
            //PropertyToChange.Add(nameof(Color));

            ExecutePropertyChange();
        }

        #endregion

        #region Prism Methods
        public override void Initialize(INavigationParameters parameters)
        {
            SearchRequest = new SearchRequest();
            SearchResponses = new List<SearchResponse>();

            Animation = AnimationConstants.Search; //default scanning animation.
            ScannerMessage = "Please enter the location you're looking for.";
            ButtonText = "Search";
            AnimationFrequency = AnimationConstants.NoLoop;
            PropertyToChange.Add(nameof(ButtonText));

            AnimationPropertyChangeApply();
        }
        #endregion
    }
}
