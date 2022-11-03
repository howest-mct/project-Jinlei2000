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
            TestKitsuRepository();
            
        }
        private async Task TestKitsuRepository()
        {
            Debug.WriteLine("TestKitsuRepository");
            List<Anime> animes = await KitsuRepository.GetAnimesAsync();
            Debug.WriteLine("xxxxxxxxxxxxxxxxxxxxx" + animes.Count);
            foreach (Anime anime in animes)
            {
                Debug.WriteLine(anime.AnimeInfo.Name);
            }
            lvwTrending.ItemsSource = animes;
        }
    }
}