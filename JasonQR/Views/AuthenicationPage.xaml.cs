using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using JasonQR.ViewModels;
using Xamarin.Forms;
using static JasonQR.ViewModels.LoginViewModel;

namespace JasonQR.Views
{
    public partial class AuthenicationPage : ContentPage
    {
        public AuthenicationPage( )
        {
            InitializeComponent();
            this.BindingContext = new AuthenticationViewModel(Navigation);
        }
        private void OtpRef_TextChanged(object sender, TextChangedEventArgs e)
        {
            const string numberRegex = @"^[0-9]+$";
            if (!string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                bool IsValid = (Regex.IsMatch(e.NewTextValue, numberRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
                ((Entry)sender).Text = IsValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            }
        }
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return false;
        }
    }
}
