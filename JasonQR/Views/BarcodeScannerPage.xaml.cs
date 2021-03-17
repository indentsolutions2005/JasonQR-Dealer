using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace JasonQR.Views
{
    public partial class BarcodeScannerPage : ZXingScannerPage
    {
        public static bool isFrom;
        public BarcodeScannerPage(bool isFromVIN)
        {
            InitializeComponent();
            isFrom = isFromVIN;
        }

        public void Click_OnScanResult(Result result)
        {
            //Navigation.PopAsync(animated: false);

            // VINPageAsync(result.Text);
           // await Application.Current.MainPage.DisplayAlert("Result", result.Text, "Ok");
            //Navigation.PopModalAsync();
           // Application.Current.MainPage.DisplayAlert("Result", result.Text, "Ok");
            // Navigation.PushAsync(new VINPage(),animated:true);

            Device.BeginInvokeOnMainThread(async () =>
            {
                if (isFrom)
                {
                    MessagingCenter.Send(this, "VINBARCODE", result.Text);
                }
                else
                {
                    MessagingCenter.Send(this, "BarodeData", result.Text);
                }
                await Navigation.PopModalAsync();
            });


        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        public async Task VINPageAsync(string str) {
          // await Application.Current.MainPage.DisplayAlert("AA", str, "Ok");
           // await Navigation.PushAsync(new VINPage(),animated:false);
        }

    }
}
