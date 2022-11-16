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

        // Update the list of favorites when the page appears
        protected override void OnAppearing()
        {
            base.OnAppearing();
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

        // Delete a favorite anime from the Favorites list
        private async void DeleteAnime(object sender, EventArgs e)
        {
            Debug.WriteLine("DeleteAnime");

            // Get the CommandParameter of the button
            Anime anime = (Anime)((Button)sender).CommandParameter;
            Debug.WriteLine(anime.Id);
            if (anime != null)
            {
                Debug.WriteLine("xxxxxxxxxxxxxxxxxxxxxx");
                await KitsuRepository.DeleteFavoriteAnimeAsync(anime.Id);
                await ShowFavAnimes();
            }
        }

        //Update the favName when the user changes it
        private async void UpdateAnime(object sender, EventArgs e)
        {
            Debug.WriteLine("UpdateAnime");
            // Get the CommandParameter of the button
            Anime anime = (Anime)((Button)sender).CommandParameter;

            string placeholder = anime.FavName;

            // show alert with input
            string result = await DisplayPromptAsync("Change favorite name", "Enter a new favorite name for this anime", "Change", "Cancel", placeholder, -1, Keyboard.Default);

            if (result != "" && result != null && result != placeholder)
            {
                // update the favName
                anime.FavName = result;

                // Update the favName
                await KitsuRepository.PutFavoriteAnimeAsync(anime);

                // Update the list of favorites
                await ShowFavAnimes();
            };
        }
    }
}