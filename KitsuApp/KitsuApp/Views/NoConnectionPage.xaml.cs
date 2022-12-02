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
            // no app shell bottom bar
            Shell.SetTabBarIsVisible(this, false);
            // no app shell top bar
            Shell.SetNavBarIsVisible(this, false);

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }
        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.None)
            {
                // go to root page and remove all pages from stack
                Navigation.PopToRootAsync();
            }
        }
    }
}