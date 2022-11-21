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
    public partial class AnimeOverview : ContentPage
    {
        public AnimeOverview()
        {
            InitializeComponent();
            // ConnectivityTest Class
            new ConnectivityTest();

            SetGenres();
            ShowAnime();
        }

        // fill the picker with genres
        private async Task SetGenres()
        {
            Debug.WriteLine("SetGenres");
            PickerGenres.ItemsSource = await KitsuRepository.GetGenresAsync();
        }

        // Show Anime by filter
        private async Task ShowAnime()
        {
            Debug.WriteLine("ShowAnime");
            cvwTrending.ItemsSource = await KitsuRepository.GetAnimesAsync(10, "trending");
            cvwPopular.ItemsSource = await KitsuRepository.GetAnimesAsync(10, "popular");
            cvwRated.ItemsSource = await KitsuRepository.GetAnimesAsync(10, "rated");
            cvwFavorite.ItemsSource = await KitsuRepository.GetAnimesAsync(10, "favorite");
            cvwUpdated.ItemsSource = await KitsuRepository.GetAnimesAsync(10, "updated");
            cvwUpcoming.ItemsSource = await KitsuRepository.GetAnimesAsync(10, "upcoming");
            cvwMovie.ItemsSource = await KitsuRepository.GetAnimesAsync(10, "movie");
        }

        // Picker selected genre & go to FilteredByGenreOverview
        private void PickerGenresSelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("Picker selected genre");
            Genre selectedGenre = (Genre)PickerGenres.SelectedItem;
            if (selectedGenre != null)
            {
                Navigation.PushAsync(new FilteredByGenrePage(selectedGenre, "anime"));
            }
            // pickerGenres back to default
            PickerGenres.SelectedIndex = -1;
        }

        // Listen to all the buttons Clicked events and go to SeeMorePage
        private void GoToSeeMorePage(object sender, EventArgs e)
        {
            Debug.WriteLine("GoToSeeMorePage");
            // get the button that was clicked
            Button button = (Button)sender;
            // Get the filter of CommandParameter (to know which filter to use)
            string filter = button.CommandParameter.ToString();
            string title = GetTitle(filter);
            Navigation.PushAsync(new SeeMorePage(filter, "anime", title));
        }

        // Get the title
        private string GetTitle(string filter)
        {
            switch (filter)
            {
                case "trending":
                    return "Trending Now";
                case "popular":
                    return "Most Popular";
                case "rated":
                    return "Highest Rated";
                case "favorite":
                    return "Best Favorite";
                case "updated":
                    return "Recently Updated";
                case "upcoming":
                    return "Top Upcoming";
                case "movie":
                    return "Top Movies";
                default:
                    return "Anime";
            }
        }

        //Listen to all the Clicked events of the CollectionView items and go to DetailPage
        private void GoToDetailPage(object sender, EventArgs e)
        {
            Debug.WriteLine("GoToDetailPage");
            // Get the selected item of the correct CollectionView 
            Collection anime = (Collection)((CollectionView)sender).SelectedItem;
            if (anime != null)
            {
                Navigation.PushAsync(new DetailPage(anime));
            }
            // Reset the selected item
            ((CollectionView)sender).SelectedItem = null;
        }


        // Add anime to favorites if not already in favorites
        private async void AddToFavorite(object sender, EventArgs e)
        {
            Debug.WriteLine("Button Add_To_Favorite");
            // Get the CommandParameter of the button
            Anime anime = (Anime)((Button)sender).CommandParameter;

            Debug.WriteLine("Anime: " + anime.Id);

            bool Check = await KitsuRepository.GetCheckFavNotExists("anime", anime.Id);
            if (Check == false)
            {
                anime.FavName = "Favorite anime";

                // Add to favorite
                await KitsuRepository.PostFavoriteAnimeAsync(anime);

                if (anime != null)
                {
                    await Navigation.PushAsync(new AnimeOverviewFav());
                }

            }
            else
            {
                await DisplayAlert("Info", "This anime is already in your favorites", "OK");
            }
        }

        // Get random anime and go to DetailPage
        private async void BtnRandomAnime(object sender, EventArgs e)
        {
            Debug.WriteLine("BtnRandomAnime");
           
            Anime anime = await KitsuRepository.GetRandomAnimeAsync("anime");
            // Loop until anime is not null
            while (anime == null)
            {
                anime = await KitsuRepository.GetRandomAnimeAsync("anime");
            }
            await Navigation.PushAsync(new DetailPage(anime));
        }
    }
}