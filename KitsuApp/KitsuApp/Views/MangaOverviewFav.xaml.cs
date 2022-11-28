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
    public partial class MangaOverviewFav : ContentPage
    {
        public MangaOverviewFav()
        {
            InitializeComponent();
        }

        // Update the list of favorites when the page appears
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // ConnectivityTest Class
            new ConnectivityTest();
            ShowFavMangas();
        }

        private async Task ShowFavMangas()
        {
            //controle doen als er geen favorites zijn dan toon je iets anders of zeg je voeg er toe
            List<Manga> favMangas = await KitsuRepository.GetFavoriteMangasAsync();
            cvwYourFavorites.IsVisible = true;
            EmptyPage.IsVisible = false;
            if (favMangas.Count == 0)
            {
                cvwYourFavorites.IsVisible = false;
                EmptyPage.IsVisible = true;
            }
            cvwYourFavorites.ItemsSource = favMangas;
        }

        // Get the selected item from the CollectionView and navigate to the DetailPage
        private void GoToDetailPage(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("GoToDetailPage");
            // Get the selected item of CollectionView cvwYourFavorites
            Collection selectedManga = (Collection)cvwYourFavorites.SelectedItem;
            if (selectedManga != null)
            {
                Navigation.PushAsync(new DetailPage(selectedManga));
            }
            // Reset the selected item
            cvwYourFavorites.SelectedItem = null;
        }

        // Delete a favorite anime from the Favorites list
        private async void DeleteManga(object sender, EventArgs e)
        {
            Debug.WriteLine("DeleteManga");

            // Get the CommandParameter of the button
            Manga manga = (Manga)((Button)sender).CommandParameter;
            Debug.WriteLine(manga.Id);
            if (manga != null)
            {
                Debug.WriteLine("xxxxxxxxxxxxxxxxxxxxxx");
                await KitsuRepository.DeleteFavoriteMangaAsync(manga.Id);
                await ShowFavMangas();
            }
        }

        //Update the favName when the user changes it
        private async void UpdateManga(object sender, EventArgs e)
        {
            Debug.WriteLine("UpdateManga");
            // Get the CommandParameter of the button
            Manga manga = (Manga)((Button)sender).CommandParameter;

            string placeholder = manga.FavName;

            // show alert with input
            string result = await DisplayPromptAsync("Change favorite name", "Enter a new favorite name for this manga", "Change", "Cancel", placeholder, -1, Keyboard.Default);

            if (result != "" && result != null && result != placeholder)
            {
                // update the favName
                manga.FavName = result;

                // Update the favName
                await KitsuRepository.PutFavoriteMangaAsync(manga);

                // Update the list of favorites
                await ShowFavMangas();
            };
        }
    }
}