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
    }
}
