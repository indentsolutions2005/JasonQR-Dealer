using System;
using System.Collections.Generic;
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
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return false;
        }
    }
}
