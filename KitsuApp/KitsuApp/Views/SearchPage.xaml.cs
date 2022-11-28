using KitsuApp.Models;
using KitsuApp.Repositories;
using KitsuApp.Services;
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
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        // Search keyword & show data
        private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            sktNothing.IsVisible = false;
            sktData.IsVisible = true;
            var search = (SearchBar)sender;
            string keyword = search.Text;

            List<Anime> animes = await KitsuRepository.SearchAnimeAsync(keyword);
            List<Manga> mangas = await KitsuRepository.SearchMangaAsync(keyword);

            // Check if there is no data
            if (animes.Count == 0 && mangas.Count == 0)
            {
                sktNothing.IsVisible = true;
                sktData.IsVisible = false;
            }
            else
            {
                cvwAnime.IsVisible = animes.Count == 0 ? false : true;
                cvwManga.IsVisible = mangas.Count == 0 ? false : true;
                lblAnime.IsVisible = animes.Count == 0 ? false : true;
                lblManga.IsVisible = mangas.Count == 0 ? false : true;
                cvwAnime.ItemsSource = animes;
                cvwManga.ItemsSource = mangas;
            }
        }

        // Empty search keyword & hide data & show sktNothing
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // ConnectivityTest Class
            new ConnectivityTest();
            sktNothing.IsVisible = true;
            sktData.IsVisible = false;
            cvwAnime.ItemsSource = null;
            cvwManga.ItemsSource = null;
            srchbar.Text = "";
        }

        //Listen to all the Clicked events of the CollectionView items and go to DetailPage
        private void GoToDetailPage(object sender, EventArgs e)
        {
            Debug.WriteLine("GoToDetailPage");
            // Get the selected item of the correct CollectionView 
            Collection collection = (Collection)((CollectionView)sender).SelectedItem;
            if (collection != null)
            {
                Navigation.PushAsync(new DetailPage(collection));
            }
            // Reset the selected item
            ((CollectionView)sender).SelectedItem = null;
        }


        // Add anime to favorites if not already in favorites
        private async void AddToFavorite(object sender, EventArgs e)
        {
            Debug.WriteLine("Button Add_To_Favorite");

            // Get the CommandParameter of the button
            Collection collection = (Collection)((Button)sender).CommandParameter;

            bool Check = collection.CollectionType == "anime" ? await KitsuRepository.GetCheckFavNotExists("anime", collection.Id) : await KitsuRepository.GetCheckFavNotExists("manga", collection.Id);
            if (Check == false)
            {
                collection.FavName = collection.CollectionType == "anime" ? "Favorite anime" : "Favorite manga";
                // Add to favorite
                if (collection.CollectionType == "anime")
                {
                    await KitsuRepository.PostFavoriteAnimeAsync((Anime)collection);
                    await Navigation.PushAsync(new AnimeOverviewFav());
                }
                else
                {
                    await KitsuRepository.PostFavoriteMangaAsync((Manga)collection);
                    await Navigation.PushAsync(new MangaOverviewFav());
                }
            }
            else
            {
                await DisplayAlert("Info", $"This {collection.CollectionType} is already in your favorites", "OK");
            }
        }
    }
}