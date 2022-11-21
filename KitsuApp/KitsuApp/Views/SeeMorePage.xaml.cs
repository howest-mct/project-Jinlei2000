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

        // Listen to the Clicked events of the CollectionView items and go to DetailPage
        private void GoToDetailPage(object sender, SelectionChangedEventArgs e)
        {
            Collection selected = (Collection)cvwSeeMore.SelectedItem;
            if (selected != null)
            {
                Navigation.PushAsync(new DetailPage(selected));
            }
            // Reset selected item
            cvwSeeMore.SelectedItem = null;
        }

        // Add anime to favorites if not already in favorites
        private async void AddToFavorite(object sender, EventArgs e)
        {
            Debug.WriteLine("Button Add_To_Favorite");
            // Get the CommandParameter of the button
            Collection collection = (Collection)((Button)sender).CommandParameter;

            if (collection.CollectionType == "anime")
            {
                Anime anime = (Anime)collection;

                bool Check = await KitsuRepository.GetCheckFavNotExists("anime", collection.Id);
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
            else if (collection.CollectionType == "manga")
            {
                //Manga manga = (Manga)collection;

                //bool Check = await KitsuRepository.GetCheckFavNotExists("manga", collection.Id);
                //if (Check == false)
                //{
                //    manga.FavName = "Favorite manga";

                //    // Add to favorite
                //    await KitsuRepository.PostFavoriteMangaAsync(manga);

                //    if (manga != null)
                //    {
                //        await Navigation.PushAsync(new AnimeOverviewFav());
                //    }

                //}
                //else
                //{
                //    await DisplayAlert("Info", "This anime is already in your favorites", "OK");
                //}
            }

        }
    }
}
