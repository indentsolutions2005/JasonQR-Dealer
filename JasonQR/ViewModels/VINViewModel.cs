using System;
using JasonQR.WebApi;
using Newtonsoft.Json;
using Xamarin.Forms;
using static JasonQR.ViewModels.AuthenticationViewModel;

namespace JasonQR.ViewModels
{
    public class VINViewModel :BaseModel
    {
        INavigation navRef;
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
        public VINViewModel(INavigation navigation)
        {
            navRef = navigation;
            IsBusy = false;
        }

        public void submitQRCode(AuthPostData jsonPost)
        {
            IsBusy = true;
            ServiceCall service = new ServiceCall();
            service.QRCodeScanned(jsonPost);
            service.qrcodeEvent += ((ServiceCall webAPISender, EventArgs e2) =>
            {
                IsBusy = false;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    try
                    {

                        string content = webAPISender.qrcodeResponse;
                        var AuthDetails = JsonConvert.DeserializeObject<AuthPostData>(webAPISender.qrcodeResponse);
                        if (AuthDetails != null && AuthDetails.text != null)
                        {
                            await App.Current.MainPage.DisplayAlert("", AuthDetails.text, "Ok");
                            if (AuthDetails.text.ToLower().Contains("not an authorized"))
                            {
                                await navRef.PopToRootAsync();
                            }
                            else
                            {
                                await navRef.PopAsync();
                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("", "Please try again", "Ok");
                        }

                    }
                    catch (Exception Ex)
                    {
                        App.Current.MainPage.DisplayAlert("", "Please try again", "OK");
                    }
                });

            });
        }
    }
}
