
using System;
using System.Text.RegularExpressions;
using JasonQR.ViewModels;
using Xamarin.Forms;

namespace JasonQR.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel(Navigation);
        }
        private void PhoneNumberRef_TextChanged(object sender, TextChangedEventArgs e)
        {
            const string numberRegex = @"^[0-9]+$";
            if (!string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                bool IsValid = (Regex.IsMatch(e.NewTextValue, numberRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
                ((Entry)sender).Text = IsValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
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
    }
}
