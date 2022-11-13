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
    public partial class FilteredByGenrePage : ContentPage
    {
        public Genre GenreContent { get; set; }
        public FilteredByGenrePage(Genre genre, string type)
        {
            InitializeComponent();
            Debug.WriteLine($"xxxxxxxxxxxxxxxxxxxxxxxxxx{type}");
            this.GenreContent = genre;
            ShowByGenre(type);
        }

        private async Task ShowByGenre(string type)
        {
            // Change Title to genre name
            Title = GenreContent.GenreInfo.Name;
            Debug.WriteLine(GenreContent.GenreInfo.Name);
            if (type == "anime")
            {
                // Get all anime from genre
                cvwSelectedGenre.ItemsSource = await KitsuRepository.GetAnimesFromGenreAsync(GenreContent.GenreInfo.Slug);
            }else if (type == "manga")
            {
                // Get all manga from genre
                cvwSelectedGenre.ItemsSource = await KitsuRepository.GetMangasFromGenreAsync(GenreContent.GenreInfo.Slug);
            }
        }

        // Listen to all the Clicked events of the CollectionView items and go to DetailPage
        private void GoToDetailPage(object sender, SelectionChangedEventArgs e)
        {
            Collection selectedAnime = (Collection)cvwSelectedGenre.SelectedItem;
            if (selectedAnime != null)
            {
                Navigation.PushAsync(new DetailPage(selectedAnime));
            }
            // Reset selected item
            cvwSelectedGenre.SelectedItem = null;
        }
    }
}