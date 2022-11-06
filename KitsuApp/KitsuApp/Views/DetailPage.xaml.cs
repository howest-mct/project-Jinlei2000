using KitsuApp.Models;
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
    public partial class DetailPage : ContentPage
    {
        public Collection CollectionContent { get; set; }
        public DetailPage(Collection collection)
        {
            InitializeComponent();
            this.CollectionContent = collection;
            ShowDetail();
        }

        private void ShowDetail()
        {
            if (CollectionContent.CollectionType == "anime")
            {
                Anime anime = (Anime)CollectionContent;
                Debug.WriteLine("ShowDetail: " + anime.AnimeInfo.Slug);
            }
            else if (CollectionContent.CollectionType == "manga")
            {
                Manga manga = (Manga)CollectionContent;
                Debug.WriteLine("ShowDetail: " + manga.MangaInfo.Slug);
            }
        }
    }
}