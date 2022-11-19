using KitsuApp.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KitsuApp.Services
{
    public class ConnectivityTest
    {
        public ConnectivityTest()
        {
            // Register for connectivity changes, be sure to unsubscribe when finished
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                // go to NoConnectionPage
                Application.Current.MainPage.Navigation.PushAsync(new NoConnectionPage());
            }
        }

        // Handle connectivity changes
        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            Debug.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxConnectivity_ConnectivityChanged");
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                // go to NoConnectionPage
                Application.Current.MainPage.Navigation.PushAsync(new NoConnectionPage());
            }
        }
    }
}
