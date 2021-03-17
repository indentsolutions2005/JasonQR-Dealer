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
            if (Application.Current.Properties.ContainsKey("Phone"))
            {
                MainPage = new NavigationPage(new ScanPage());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
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
