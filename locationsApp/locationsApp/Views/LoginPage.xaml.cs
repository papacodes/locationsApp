using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using locationsApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace locationsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ClientIdEntry.Focused += InputFocused;
            ClientSecretEntry.Focused += InputFocused;
            ClientIdEntry.Unfocused += InputUnfocused;
            ClientSecretEntry.Unfocused += InputUnfocused;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ClientIdEntry.Focused -= InputFocused;
            ClientSecretEntry.Focused -= InputFocused;
            ClientIdEntry.Unfocused -= InputUnfocused;
            ClientSecretEntry.Unfocused -= InputUnfocused;
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