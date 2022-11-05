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
    public partial class FilteredByGenrePage : ContentPage
    {
        public Genre genreContent { get; set; }
        public FilteredByGenrePage(Genre genre, string type)
        {
            InitializeComponent();
            Debug.WriteLine($"xxxxxxxxxxxxxxxxxxxxxxxxxx{type}");
            this.genreContent = genre;
            ShowByGenre(type);
        }

        private async Task ShowByGenre(string type)
        {
            // Change Title to genre name
            Title = genreContent.GenreInfo.Name;
            Debug.WriteLine(genreContent.GenreInfo.Name);
            if (type == "anime")
            {
                // Get all anime from genre
                cvwSelectedGenre.ItemsSource = await KitsuRepository.GetAnimesFromGenreAsync(genreContent.GenreInfo.Slug);
            }else if (type == "manga")
            {
                // Get all manga from genre
                cvwSelectedGenre.ItemsSource = await KitsuRepository.GetMangasFromGenreAsync(genreContent.GenreInfo.Slug);
            }
        }
    }
}