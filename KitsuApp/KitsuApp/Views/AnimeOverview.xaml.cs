﻿using KitsuApp.Models;
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

        // Listen to all the buttons Clicked events and go to SeeMorePage
        private void GoToSeeMorePage(object sender, EventArgs e)
        {
            Debug.WriteLine("GoToSeeMorePage");
            Button button = (Button)sender;
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

        // Listen to all the Clicked events of the CollectionView items and go to DetailPage
        private void GoToDetailPage(object sender, EventArgs e)
        {
            Debug.WriteLine("GoToDetailPage");
            Collection anime = (Collection)((CollectionView)sender).SelectedItem;
            if (anime != null)
            {
                Navigation.PushAsync(new DetailPage(anime));
            }
        }

    }
}