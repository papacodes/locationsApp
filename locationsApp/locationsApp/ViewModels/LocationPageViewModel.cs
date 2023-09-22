using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using locationsApp.Constants;
using locationsApp.Models.Requests;
using locationsApp.Models.Responses.LocationResponse;
using locationsApp.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace locationsApp.ViewModels
{
	public class LocationPageViewModel : BaseViewModel
	{

        #region Private Properties

        #endregion

        #region Public Properties
        public LocationResponse locationResponse { get; set; }
        public List<PhotoResponse> Photos { get; set; }
        public IList<ImageSource> Images { get; set; }
        #endregion

        #region Constructor
        public LocationPageViewModel
        (
            INavigationService navigationService
        )
            : base(navigationService)
        {
            locationResponse = new LocationResponse();
            Photos = new List<PhotoResponse>();

            
            locationResponse = JsonConvert.DeserializeObject<LocationResponse>(Preferences.Get(SharedPreferencesKeys.LocationKey, SharedPreferencesKeys.Getter));
            Photos = JsonConvert.DeserializeObject<List<PhotoResponse>>(Preferences.Get(SharedPreferencesKeys.PhotosKey, SharedPreferencesKeys.Getter));
        }
        #endregion

        #region Prism methods
        public override void Initialize(INavigationParameters parameters)
        {
            //was trying to get the images to display but not enough time. :-(
            //foreach (var photo in Photos)
            //{

            //    var tempImage = ImageSource.FromStream(
            //        () => new MemoryStream(Convert.FromBase64String(photo.photo)));

            //    Images.Add(tempImage);
            //}

            //PropertyToChange.Add(nameof(Images));
            //ExecutePropertyChange();
        }
        #endregion
    }
}

