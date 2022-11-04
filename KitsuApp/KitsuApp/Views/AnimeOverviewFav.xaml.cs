using DLToolkit.Forms.Controls;
using KitsuApp.Models;
using KitsuApp.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KitsuApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnimeOverviewFav : ContentPage
    {
        public AnimeOverviewFav()
        {
            InitializeComponent();
            ShowFavAnimes();
        }

        private async Task ShowFavAnimes()
        {
            Debug.WriteLine("ShowFavAnimes");
            //controle doen als er geen favorites zijn dan toon je iets anders of zeg je voeg er toe
            cvwYourFavorites.ItemsSource = "";
        }
    }
}