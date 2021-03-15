using System;
using System.Windows.Input;
using JasonQR.Models;
using JasonQR.Views;
using JasonQR.WebApi;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace JasonQR.ViewModels
{

    public class LoginViewModel : BaseModel
    {

        public class LoginPostData
        {
            public string type { get; set; }
            public string time { get; set; }
            public string from { get; set; }
            public string to { get; set; }
            public string text { get; set; }
        }

        INavigation naviRef;
        public ICommand LoginCommand { private set; get; }

        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;

                OnPropertyChanged("PhoneNumber");
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

        public LoginViewModel(INavigation navigation)
        {
            naviRef = navigation;
            //_isBusy = true;

            //Constants cons = new Constants();






            LoginCommand = new Command(() => {
                // naviRef.PushAsync(new AuthenicationPage());
                IsBusy = true;
                Constants.MobileNumber = PhoneNumber;

                LoginPostData jsonPost = new LoginPostData();
                jsonPost.type = "message-received";
                jsonPost.time = "anytime";
                jsonPost.from = PhoneNumber;
                jsonPost.to = "+19252177190";
                jsonPost.text = "Authenticate";


                ServiceCall service = new ServiceCall();
                service.GetLoginDetails(jsonPost);
                service.loginEvent += ((ServiceCall webAPISender, EventArgs e2) =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        try
                        {

                            string content = webAPISender.loginResponse;
                            var loginResponseDetails = JsonConvert.DeserializeObject<LoginPostData>(webAPISender.loginResponse);
                            IsBusy = false;
                            if (loginResponseDetails.text == "Authentication code sent")
                            {
                                naviRef.PushAsync(new AuthenicationPage());
                            }
                            else
                            {
                                App.Current.MainPage.DisplayAlert("", "Please try again", "OK");

                            }
                            // Debug.WriteLine("the data is", +NewOrgDetails.success);

                        }
                        catch (Exception Ex)
                        {
                            IsBusy = false;
                            App.Current.MainPage.DisplayAlert("", "Please try again", "OK");
                        }
                    });

                });

            });
        }
    }
}
