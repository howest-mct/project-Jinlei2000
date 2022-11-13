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
            List<Anime> favAnimes = await KitsuRepository.GetFavoriteAnimesAsync();
            Debug.WriteLine("favAnimes.Count: " + favAnimes.Count);
            cvwYourFavorites.IsVisible = true;
            EmptyPage.IsVisible = false;
            if (favAnimes.Count == 0)
            {
                cvwYourFavorites.IsVisible = false;
                EmptyPage.IsVisible = true;
            }
            cvwYourFavorites.ItemsSource = favAnimes;
        }

        // Get the selected item from the CollectionView and navigate to the DetailPage
        private void GoToDetailPage(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("GoToDetailPage");
            // Get the selected item of CollectionView cvwYourFavorites
            Collection selectedAnime = (Collection)cvwYourFavorites.SelectedItem;
            if (selectedAnime != null)
            {
                Navigation.PushAsync(new DetailPage(selectedAnime));
            }
            // Reset the selected item
            cvwYourFavorites.SelectedItem = null;
        }
    }
}