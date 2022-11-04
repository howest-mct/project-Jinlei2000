using DLToolkit.Forms.Controls;
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
            ShowAnime();

        }
        private async Task ShowAnime()
        {
            Debug.WriteLine("TestKitsuRepository");
            cvwTrending.ItemsSource = await KitsuRepository.GetAnimesAsync(10, "trending");
            cvwPopular.ItemsSource = await KitsuRepository.GetAnimesAsync(10, "popular");
            cvwRated.ItemsSource = await KitsuRepository.GetAnimesAsync(10, "rated");
            cvwFavorite.ItemsSource = await KitsuRepository.GetAnimesAsync(10, "favorite");
            cvwUpdated.ItemsSource = await KitsuRepository.GetAnimesAsync(10, "updated");
            cvwUpcoming.ItemsSource = await KitsuRepository.GetAnimesAsync(10, "upcoming");
            cvwMovie.ItemsSource = await KitsuRepository.GetAnimesAsync(10, "movie");
        }
    }
}