using KitsuApp.Models;
using KitsuApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KitsuApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeeMorePage : ContentPage
    {
        public SeeMorePage(string filter, string type, string title)
        {
            InitializeComponent();
            ShowMore(filter, type, title);
        }

        private async Task ShowMore(string filter, string type, string title)
        {
            Title = title;

            if (type == "anime")
            {
                cvwSeeMore.ItemsSource = await KitsuRepository.GetAnimesAsync(20, filter);
            }
            else if (type == "manga")
            {
                cvwSeeMore.ItemsSource = await KitsuRepository.GetMangasAsync(20, filter);
            }
        }

    }
}
