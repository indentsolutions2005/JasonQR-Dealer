#region "Copyright Nestle Software development 2017"
/*
     All rights are reserved.Reproduction or transmission in whole or 
     in part, in any form or by any means, electronic, mechanical or 
     otherwise, is prohibited without the prior written consent of the 
     copyright owner.
*/
#endregion
using Android.App;
using Android.OS;

namespace JasonQR.Droid
{
    [Activity(Theme = "@style/Theme.Splash",
                MainLauncher = true,
                Icon = "@mipmap/AppIcon",
                NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.StartActivity(typeof(MainActivity));
        }
    }
}