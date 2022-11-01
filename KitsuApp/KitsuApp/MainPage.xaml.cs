using KitsuApp.Models;
using KitsuApp.Repositories;
using KitsuApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KitsuApp
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            //TestKitsuRepository();
            //Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            //CheckConnectivity();
        }
        
        //private async Task TestKitsuRepository()
        //{
        //    Debug.WriteLine("TestKitsuRepository");
        //    List<Anime> animes = await KitsuRepository.GetAnimesAsync();
        //    Debug.WriteLine("xxxxxxxxxxxxxxxxxxxxx"+animes.Count);
        //    foreach (Anime anime in animes)
        //    {
        //        Debug.WriteLine(anime.TotalTime);
        //    }
        //}

        //private void CheckConnectivity()
        //{
        //    if (Connectivity.NetworkAccess == NetworkAccess.None)
        //    {
        //        Navigation.PushAsync(new NoConnectionPage());
        //    }
        //}

        //void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        //{
        //    Debug.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxConnectivity_ConnectivityChanged");
        //    if (Connectivity.NetworkAccess == NetworkAccess.None)
        //    {
        //        Navigation.PushAsync(new NoConnectionPage());
        //    }
        //}
    }
}
