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
    public partial class MangaOverview : ContentPage
    {
        public MangaOverview()
        {
            InitializeComponent();
            //SetGenres();
            //ShowManga();
        }

        // OnAppearing
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // ConnectivityTest Class
            new ConnectivityTest();
            SetGenres();
            ShowManga();
        }

        // fill the picker with genres
        private async Task SetGenres()
        {
            Debug.WriteLine("SetGenres");
            PickerGenres.ItemsSource = await KitsuRepository.GetGenresAsync();
        }

        // Show Anime by filter
        private async Task ShowManga()
        {
            Debug.WriteLine("ShowManga");
            cvwTrending.ItemsSource = await KitsuRepository.GetMangasAsync(10, "trending");
            cvwPopular.ItemsSource = await KitsuRepository.GetMangasAsync(10, "popular");
            cvwRated.ItemsSource = await KitsuRepository.GetMangasAsync(10, "rated");
            cvwFavorite.ItemsSource = await KitsuRepository.GetMangasAsync(10, "favorite");
            cvwUpdated.ItemsSource = await KitsuRepository.GetMangasAsync(10, "updated");
            cvwUpcoming.ItemsSource = await KitsuRepository.GetMangasAsync(10, "upcoming");
            cvwManhua.ItemsSource = await KitsuRepository.GetMangasAsync(10, "manhua");
        }

        // Picker selected genre & go to FilteredByGenreOverview
        private void PickerGenresSelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("Picker selected genre");
            Genre selectedGenre = (Genre)PickerGenres.SelectedItem;
            if (selectedGenre != null)
            {
                Navigation.PushAsync(new FilteredByGenrePage(selectedGenre, "manga"));
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
            Navigation.PushAsync(new SeeMorePage(filter, "manga", title));
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
                    return "Top Manhua";
                default:
                    return "Manga";
            }
        }

        //Listen to all the Clicked events of the CollectionView items and go to DetailPage
        private void GoToDetailPage(object sender, EventArgs e)
        {
            Debug.WriteLine("GoToDetailPage");
            // Get the selected item of the correct CollectionView 
            Collection manga = (Collection)((CollectionView)sender).SelectedItem;
            if (manga != null)
            {
                Navigation.PushAsync(new DetailPage(manga));
            }
            // Reset the selected item
            ((CollectionView)sender).SelectedItem = null;
        }


        // Add manga to favorites if not already in favorites
        private async void AddToFavorite(object sender, EventArgs e)
        {
            Debug.WriteLine("Button Add_To_Favorite");
            // Get the CommandParameter of the button
            Manga manga = (Manga)((Button)sender).CommandParameter;

            Debug.WriteLine("Manga: " + manga.Id);

            bool Check = await KitsuRepository.GetCheckFavNotExists("manga", manga.Id);
            if (Check == false)
            {
                manga.FavName = "Favorite manga";

                // Add to favorite
                await KitsuRepository.PostFavoriteMangaAsync(manga);

                if (manga != null)
                {
                    await Navigation.PushAsync(new MangaOverviewFav());
                }

            }
            else
            {
                await DisplayAlert("Info", "This manga is already in your favorites", "OK");
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