using KitsuApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//icons fonts
[assembly: ExportFont("MaterialIconsRegular.ttf", Alias = "MaterialIconsRegular")]
[assembly: ExportFont("MaterialIconsTwoToneRegular.otf", Alias = "MaterialIconsTwoToneRegular")]

//lettertype fonts
[assembly: ExportFont("InterRegular.ttf", Alias = "InterRegular")]
[assembly: ExportFont("InterMedium.ttf", Alias = "InterMedium")]
[assembly: ExportFont("InterSemiBold.ttf", Alias = "InterSemiBold")]
[assembly: ExportFont("InterBold.ttf", Alias = "InterBold")]

namespace KitsuApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
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
