using System;
using System.Windows.Input;
using JasonQR.Models;
using JasonQR.Views;
using JasonQR.WebApi;
using Newtonsoft.Json;
using Xamarin.Forms;
using static JasonQR.ViewModels.AuthenticationViewModel;

namespace JasonQR.ViewModels
{
    public class ScanViewModel : BaseModel
    {
        public ICommand ScanCommand { private set; get; }
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }
        INavigation navRef;
        public ScanViewModel(INavigation navigation)
        {
            navRef = navigation;
            ScanCommand = new Command(() =>
            {
                navigation.PushModalAsync(new BarcodeScannerPage(false));
              //  navigation.PushAsync(new VINPage());

            });

            MessagingCenter.Subscribe<BarcodeScannerPage, string>(this, "BarodeData", async (sender, arg) =>
            {
               // Application.Current.MainPage.DisplayAlert("Result", arg, "Ok");
                //subbu
                if (!string.IsNullOrEmpty(arg))
                {
                    //getQRCOdeData(arg.ToString());
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        navRef.PushAsync(new VINPage(arg));
                    });
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("","Empty Result","Ok");
                }
                
            });

            
        }

        
    }
}
