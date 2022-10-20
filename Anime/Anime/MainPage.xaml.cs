using Anime.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Anime
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            CheckConnectivity();
        
        }

        private void CheckConnectivity()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                Navigation.PushAsync(new NoConnectionPage());
            }
        }

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            Debug.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxConnectivity_ConnectivityChanged");
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                Navigation.PushAsync(new NoConnectionPage());
            }
        }
    }
}
