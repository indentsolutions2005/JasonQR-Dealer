using System;
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
           /* System.Threading.Tasks.Task.Delay(20 * 1000).ContinueWith((_) => StartVoid());
            if (isFrom)
            {
                scannerRef.DefaultOverlayTopText = "Align the Barcode within the frame";
            }*/
        }

       /* private void StartVoid()
        {
            try
            {
              Device.BeginInvokeOnMainThread(() =>
               {
                   Navigation.PopModalAsync();
               });
            }catch(Exception ex)
            {

            }
        }*/

        public void Click_OnScanResult(Result result)
        {
            //Navigation.PopAsync(animated: false);

            // VINPageAsync(result.Text);
            // await Application.Current.MainPage.DisplayAlert("Result", result.Text, "Ok");
            //Navigation.PopModalAsync();
            // Application.Current.MainPage.DisplayAlert("Result", result.Text, "Ok");
            // Navigation.PushAsync(new VINPage(),animated:true);
            ZXing.BarcodeFormat barcodeFormat = result.BarcodeFormat;

            Device.BeginInvokeOnMainThread(async () =>
            {
                if (isFrom)
                {
                    if (!barcodeFormat.ToString().Contains("QR"))
                    {
                        MessagingCenter.Send(this, "VINBARCODE", result.Text);
                    }
                }
                else
                {
                    MessagingCenter.Send(this, "BarodeData", result.Text);
                }
                await Navigation.PopModalAsync();
            });


        }
        
        public async Task VINPageAsync(string str) {
          // await Application.Current.MainPage.DisplayAlert("AA", str, "Ok");
           // await Navigation.PushAsync(new VINPage(),animated:false);
        }

    }
}
