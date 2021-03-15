using System;
using JasonQR.Views;
using Xamarin.Forms;


namespace JasonQR
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
            //MainPage = new NavigationPage(new VINPage("1234"));
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.DarkRed;
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;


        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
