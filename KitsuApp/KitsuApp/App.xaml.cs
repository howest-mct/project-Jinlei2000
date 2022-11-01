using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KitsuApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex("#FCFDFE"),
                BarTextColor = Color.FromHex("#4056F4"),
            };
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
