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
    public partial class AnimeOverview : ContentPage
    {
        public AnimeOverview()
        {
            InitializeComponent();
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
        private void PickerGenres_SelectedIndexChanged(object sender, EventArgs e)
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

        // Go to SeeMorePage
        private void GoToSeeMorePage(string filter, string title)
        {
            Debug.WriteLine("GoToSeeMorePage");
            Navigation.PushAsync(new SeeMorePage(filter, "anime", title));
        }

        // Listen to the Clicked event of the Trending button
        private void Button_Clicked_Trending(object sender, EventArgs e)
        {
            GoToSeeMorePage("trending", "Trending Now");
        }

        // Listen to the Clicked event of the Popular button
        private void Button_Clicked_Popular(object sender, EventArgs e)
        {
            GoToSeeMorePage("popular", "Most Popular");
        }

        // Listen to the Clicked event of the Rated button
        private void Button_Clicked_Rated(object sender, EventArgs e)
        {
            GoToSeeMorePage("rated", "Highest Rated");
        }

        // Listen to the Clicked event of the Favorite button
        private void Button_Clicked_Favorite(object sender, EventArgs e)
        {
            GoToSeeMorePage("favorite", "Best Favorite");
        }

        // Listen to the Clicked event of the Updated button
        private void Button_Clicked_Updated(object sender, EventArgs e)
        {
            GoToSeeMorePage("updated", "Recently Updated");
        }

        // Listen to the Clicked event of the Upcoming button
        private void Button_Clicked_Upcoming(object sender, EventArgs e)
        {
            GoToSeeMorePage("upcoming", "Top Upcoming");
        }

        // Listen to the Clicked event of the Movie button
        private void Button_Clicked_Movies(object sender, EventArgs e)
        {
            GoToSeeMorePage("movie", "Top Movies");
        }
    }
}