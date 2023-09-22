using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using locationsApp.Models;
using locationsApp.Services;
using Prism.Mvvm;
using Prism.Navigation;

namespace locationsApp.ViewModels
{
    public abstract class BaseViewModel : BindableBase, IInitialize, INotifyPropertyChanged
    {
        protected INavigationService NavigationService { get; }

        public List<string> PropertyToChange { get; set; }

        public bool isBusy { get; set; }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        #region events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region constructor
        public BaseViewModel
        (
            INavigationService NavigationService
        )
        {
            this.NavigationService = NavigationService;
            PropertyToChange = new List<string>();
            isBusy = false;
        }
        #endregion

        #region property changed manual
        protected void ExecutePropertyChange()
        {
            if (PropertyToChange != null)
            {
                foreach (var value in PropertyToChange)
                {
                    OnPropertyChanged(value);
                }

                PropertyToChange.Clear();

            }

        }
        #endregion

        #region INotifyPropertyChanged
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChangedd([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void ExecuteLoader()
        {
            if (isBusy)
            {
                isBusy = false;
                PropertyToChange.Add(nameof(isBusy));
                ExecutePropertyChange();
            }
            else
            {
                isBusy = true;
                PropertyToChange.Add(nameof(isBusy));
                ExecutePropertyChange();

            }
        }
        #endregion

        #region Prism Methods
        public abstract void Initialize(INavigationParameters parameters);
        #endregion
    }
}
