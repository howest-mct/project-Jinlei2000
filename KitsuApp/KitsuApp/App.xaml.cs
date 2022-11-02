using KitsuApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("MaterialIconsRegular.ttf", Alias = "MaterialIconsRegular")]
[assembly: ExportFont("MaterialIconsTwoToneRegular.otf", Alias = "MaterialIconsTwoToneRegular")]

namespace KitsuApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new MainPage())
            //{
            //    BarBackgroundColor = Color.FromHex("#FCFDFE"),
            //    BarTextColor = Color.FromHex("#4056F4"),
            //};
            MainPage = new AppShell();
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
