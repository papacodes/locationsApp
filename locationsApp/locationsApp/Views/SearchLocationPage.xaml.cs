using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using locationsApp.Models.Responses.LocationResponse;
using locationsApp.Services.Interfaces;
using locationsApp.ViewModels;
using Prism.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace locationsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchLocationPage : ContentPage
    {
        public SearchLocationPage(INavigationService navigationService, ILocationService locationService)
        {
            InitializeComponent();

            var viewModel = new SearchLocationPageViewModel(navigationService, locationService);
            viewModel.OnSearchResultEvent += OnSearchResultEventAsync;
            BindingContext = viewModel;
        }

        public void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var itemSelected = (SearchResponse)e.SelectedItem;

            if (itemSelected != null)
            {
                (BindingContext as SearchLocationPageViewModel).LocationSelector.Execute(itemSelected);

                (sender as ListView).SelectedItem = null;
            }
        }

        public async void OnSearchResultEventAsync(object sender, EventArgs e)
        {
            await scrollview.ScrollToAsync((StackLayout)results, ScrollToPosition.Start, true);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            searchEntry.Focused += InputFocused;
            searchEntry.Unfocused += InputUnfocused;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            searchEntry.Focused -= InputFocused;
            searchEntry.Unfocused -= InputUnfocused;
        }

        void InputFocused(object sender, EventArgs args)
        {
            Content.LayoutTo(new Rectangle(0, -200, Content.Bounds.Width, Content.Bounds.Height));
        }

        void InputUnfocused(object sender, EventArgs args)
        {
            Content.LayoutTo(new Rectangle(0, 0, Content.Bounds.Width, Content.Bounds.Height));
        }

    }
}