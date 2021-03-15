using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using JasonQR.WebApi;
using Newtonsoft.Json;
using Tesseract;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;
using static JasonQR.ViewModels.AuthenticationViewModel;
using System.Threading.Tasks;
using JasonQR.Models;
using ZXing.Net.Mobile.Forms;

namespace JasonQR.Views
{
    public partial class VINPage : ContentPage
    {
        public string qrcodeStr;

        private Image _takenImage;

        private readonly ITesseractApi _tesseractApi;
        private readonly IDevice _device;
        public String vinNumber;

        public VINPage(String qrcodeString)
        {
            InitializeComponent();
            qrcodeStr = qrcodeString;
            submitRef.Clicked += SubmitRef_Clicked;
            scanVinBarcode.Clicked += ScanVinBarcode_Clicked;
            scanVinNumber.Clicked += ScanVinNumber_Clicked;
            vinNumberRef.TextChanged += VinNumberRef_TextChanged;

            _tesseractApi = Resolver.Resolve<ITesseractApi>();
            _device = Resolver.Resolve<IDevice>();

            MessagingCenter.Subscribe<BarcodeScannerPage, string>(this, "VINBARCODE", async (sender, arg) =>
            {
                //getQRCOdeData(arg.ToString());
                vinNumber = arg.ToString();
            });
        }

        private void VinNumberRef_TextChanged(object sender, TextChangedEventArgs e)
        {
            const string numberRegex = @"^[a-zA-Z0-9]+$";
            if (!string.IsNullOrWhiteSpace(e.NewTextValue))
            {
               bool IsValid = (Regex.IsMatch(e.NewTextValue, numberRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
               ((Entry)sender).Text = IsValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }

        private void ScanVinNumber_Clicked(object sender, EventArgs e)
        {
            getData();
        }

        public async void getData()
        {
            loading.IsVisible = true;
            scanVinNumber.IsEnabled = false;

            //if (!_tesseractApi.Initialized)
               await _tesseractApi.Init("eng");


            var photo = await TakePic();
            if (photo != null)
            {
                // When setting an ImageSource using a stream, 
                // the stream gets closed, so to avoid that I backed up
                // the image into a byte array with the following code:
                var imageBytes = new byte[photo.Source.Length];
                photo.Source.Position = 0;
                photo.Source.Read(imageBytes, 0, (int)photo.Source.Length);
                photo.Source.Position = 0;

                var testResult = await _tesseractApi.SetImage(imageBytes);

                if (testResult)
                {
                    var resultString = _tesseractApi.Text;
                    vinNumber = resultString;
                    //getQRCOdeData(resultString);
                }
                else
                {
                    DisplayAlert("", "Please try again", "Ok");
                }
            }

            loading.IsVisible = false;
            scanVinNumber.IsEnabled = true;
        }

        private void ScanVinBarcode_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new BarcodeScannerPage(true));
        }

        private void SubmitRef_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(vinNumberRef.Text))
            {
                //if (vinNumberRef.Text.Length > 6)
                //{
                //    var lastSixDigits = vinNumberRef.Text.Substring(vinNumberRef.Text.Length - 6);
                //    getQRCOdeData(lastSixDigits);
                //}
                //else
                //{
                    getQRCOdeData(vinNumberRef.Text);
                //}

            }
            else if (!String.IsNullOrEmpty(vinNumber))
            {
                getQRCOdeData(vinNumber);
            }
            else
            {
                DisplayAlert("", "Please select Anyone option", "Ok");
            }
            
            
        }

        private async Task<MediaFile> TakePic()
        {
            var mediaStorageOptions = new CameraMediaStorageOptions
            {
                DefaultCamera = CameraDevice.Rear
            };
            var mediaFile = await _device.MediaPicker.TakePhotoAsync(mediaStorageOptions);

            return mediaFile;
        }

        public void getQRCOdeData(string vinNumber)
        {
            loading.IsVisible = true;
            //string str = "Click send to learn about this vehicle QR-" + QRCodeResult;
            AuthPostData authPost = new AuthPostData();
            authPost.type = "message-received";
            authPost.time = DateTime.Now.ToString();
            authPost.from = Constants.MobileNumber;
            authPost.to = "+19252177190";
            if (vinNumberRef.Text.Length > 6)
            {
                var lastSixDigits = vinNumberRef.Text.Substring(vinNumberRef.Text.Length - 6);
                authPost.text = qrcodeStr + " " + lastSixDigits;
            }
            else
            {
                authPost.text = qrcodeStr + " " + vinNumber;
            }

            ServiceCall service = new ServiceCall();
            service.QRCodeScanned(authPost);
            service.qrcodeEvent += ((ServiceCall webAPISender, EventArgs e2) =>
            {

                loading.IsVisible = false;
                    try
                    {
                    Device.BeginInvokeOnMainThread(async() =>
                    {
                        var AuthDetails = JsonConvert.DeserializeObject<AuthPostData>(webAPISender.qrcodeResponse);
                        if (AuthDetails != null && AuthDetails.text != null)
                        {
                          await DisplayAlert("", AuthDetails.text, "Ok");
                            if (AuthDetails.text.ToLower().Contains("not an authorized")) {
                                await Navigation.PopToRootAsync();
                            }
                            else {
                                await Navigation.PopAsync();
                            }
                        }
                        else
                        {
                            await DisplayAlert("","Please try again", "Ok");
                        }
                    });

                    }
                    catch (Exception ex)
                    {
                      DisplayAlert("", "Please try again", "Ok");
                     }
            });

        }
    }
}
