using System;
using System.Windows.Input;
using JasonQR.Models;
using JasonQR.Views;
using JasonQR.WebApi;
using Newtonsoft.Json;
using Xamarin.Forms;
using static JasonQR.ViewModels.LoginViewModel;

namespace JasonQR.ViewModels
{
    public class AuthenticationViewModel : BaseModel
    {

        public class AuthPostData
        {
            public string type { get; set; }
            public string time { get; set; }
            public string from { get; set; }
            public string to { get; set; }
            public string text { get; set; }
        }
        public ICommand OTPCommand { private set; get; }

        private string _oTP;
        public string OTP
        {
            get { return _oTP; }
            set
            {
                _oTP = value;

                OnPropertyChanged("OTP");
            }
        }

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

        INavigation naviRef;
        public AuthenticationViewModel(INavigation navigation)
        {
            naviRef = navigation;
           
            OTPCommand = new Command(() =>
            {
                //Application.Current.Properties["Phone"] = Constants.MobileNumber;
                //Application.Current.SavePropertiesAsync();
                //navigation.PushAsync(new ScanPage(), false);


                IsBusy = true;
                string str = "Response " + OTP;
                AuthPostData authPost = new AuthPostData();
                authPost.type = "message-received";
                authPost.time = "anytime";
                authPost.from = Constants.MobileNumber;
                authPost.to = "+19252177190";
                authPost.text = str;

                ServiceCall service = new ServiceCall();
                service.GetAuthenticationDetails(authPost);
                service.authEvent += ((ServiceCall webAPISender, EventArgs e2) =>
                {
                    IsBusy = false;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        try
                        {
                           // string content = webAPISender.authResponse;
                            var AuthDetails = JsonConvert.DeserializeObject<AuthPostData>(webAPISender.authResponse);
                            if (AuthDetails.text.ToLower().Contains("valid"))
                            {
                                Application.Current.Properties["Phone"] = Constants.MobileNumber;
                                Application.Current.SavePropertiesAsync();
                                navigation.PushAsync(new ScanPage(), false);
                            }
                            else
                            {
                            App.Current.MainPage.DisplayAlert("", "Please try again", "OK");
                            }

                        }
                        catch (Exception e)
                        {
                        App.Current.MainPage.DisplayAlert("", "Please try again", "OK");
                        }
                    });
                });


            });
        }
    }
}
