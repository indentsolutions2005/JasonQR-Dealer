using System;
using System.Collections.Generic;
using JasonQR.ViewModels;
using Xamarin.Forms;

namespace JasonQR.Views
{
    public partial class ScanPage : ContentPage
    {
        public ScanPage()
        {
            InitializeComponent();
            this.BindingContext = new ScanViewModel(Navigation);
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasBackButton(this, false);
        }
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return false;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<BarcodeScannerPage>(this, "BarodeData");
        }
    }
}
