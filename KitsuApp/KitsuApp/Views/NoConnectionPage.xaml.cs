using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KitsuApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoConnectionPage : ContentPage
    {
        public NoConnectionPage()
        {
            InitializeComponent();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            ImageConnnection.Source = ImageSource.FromResource("KitsuApp.Assets.no-internet.png");

        }
        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            Debug.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxConnectivity_ConnectivityChanged");
            if (Connectivity.NetworkAccess != NetworkAccess.None)
            {
                Navigation.PushAsync(new MainPage());
            }
        }
    }
}