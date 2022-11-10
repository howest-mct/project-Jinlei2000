using KitsuApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Xml.Linq;
using Xamarin.Essentials;
using KitsuApp.Repositories;

namespace KitsuApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        public Collection CollectionContent { get; set; }
        public string TrailerLink { get; set; }
        public DetailPage(Collection collection)
        {
            InitializeComponent();
            this.CollectionContent = collection;
            ShowDetail();
        }

        private async Task ShowDetail()
        {
            if (CollectionContent.CollectionType == "anime")
            {
                Anime anime = (Anime)CollectionContent;
                Debug.WriteLine("ShowDetail: " + anime.AnimeInfo.Name);
                lblName.Text = anime.AnimeInfo.Name;
                lblRating.Text = anime.Rating.ToString();
                imgPoster.Source = anime.AnimeInfo.PosterImage.Medium;
                lblTotalTime.Text = anime.TotalTime;
                lblEpisodes.Text = anime.AnimeInfo.EpisodeCount == 0 || anime.AnimeInfo.EpisodeCount == 1 ? "N/A" : anime.AnimeInfo.EpisodeCount.ToString();
                lblDuration.Text = anime.AnimeInfo.EpisodeLength == 0 || anime.AnimeInfo.EpisodeCount == 1 ? "N/A" : anime.AnimeInfo.EpisodeLength.ToString() + "m per ep";
                lblType.Text = anime.AnimeInfo.Subtype;
                lblStatus.Text = anime.AnimeInfo.Status.Substring(0, 1).ToUpper() + anime.AnimeInfo.Status.Substring(1);
                lblAired.Text = anime.Aired;
                lblSynopsis.Text = anime.AnimeInfo.Synopsis == null ? "No Description" : anime.AnimeInfo.Synopsis;

                lblSeason.Text = anime.Season;
                lblHighestRatedRank.Text = anime.AnimeInfo.HighestRatedRank == null ? "#?" : $"#{anime.AnimeInfo.HighestRatedRank}";
                lblPopularityRank.Text = anime.AnimeInfo.PopularityRank == null ? "#?" : $"#{anime.AnimeInfo.PopularityRank}";
                lblMembers.Text = anime.AnimeInfo.Members == null ? "0" : anime.AnimeInfo.Members;
                lblAgeRating.Text = anime.AgeRating;


                if (anime.TrailerLink == "N/A")
                {
                    btnTrailer.IsVisible = false;
                }
                TrailerLink = anime.TrailerLink;

                
                List<Genre> genres = await KitsuRepository.GetGenresFromAnimeIDAsync(anime.Id);
                Debug.WriteLine("Genres: " + genres.Count);
                if (genres.Count == 0)
                {
                    cvwGenre.IsVisible = false;
                    lblGenre.IsVisible = false;
                }
                else
                {
                    cvwGenre.ItemsSource = genres;
                }



            }
            else if (CollectionContent.CollectionType == "manga")
            {
                Manga manga = (Manga)CollectionContent;
                Debug.WriteLine("ShowDetail: " + manga.MangaInfo.Slug);

                btnTrailer.IsVisible = false;

            }
        }


        private void canvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            float degree = 0;
            if (CollectionContent.CollectionType == "anime")
            {
                Anime anime = (Anime)CollectionContent;
                degree = anime.Rating != 0 ? anime.Rating * (360 / 10) : 0;
            }
            else if (CollectionContent.CollectionType == "manga")
            {
                Manga manga = (Manga)CollectionContent;
                degree = manga.Rating != 0 ? manga.Rating * (360 / 10) : 0;
            }

            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear(SKColors.White);

            // Set transfroms
            canvas.Translate(e.Info.Width / 2, e.Info.Height / 2);
            canvas.Scale(e.Info.Width / 200f);

            // background color of the circle
            SKPaint skPaintGray = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Gray,
                StrokeWidth = 15,
                IsAntialias = true,
            };

            // Draw Arc
            SKPaint skPaint = new SKPaint()
            {
                Style = SKPaintStyle.Stroke,
                Color = new SKColor(15, 255, 80),
                StrokeWidth = 15,
                IsAntialias = true,
            };

            SKRect skRectangle = new SKRect();
            skRectangle.Size = new SKSize(180, 180);
            skRectangle.Location = new SKPoint(-180f / 2, -180f / 2);

            float startAngle = -90;
            float sweepAngle = degree; //Calculate degrees

            SKPath skPath = new SKPath();
            skPath.AddArc(skRectangle, startAngle, sweepAngle);

            SKPath sKPathGray = new SKPath();
            sKPathGray.AddArc(skRectangle, startAngle, 360);

            canvas.DrawPath(sKPathGray, skPaintGray);

            canvas.DrawPath(skPath, skPaint);
        }

        private void Button_Trailer_Clicked(object sender, EventArgs e)
        {
            Uri uri = new Uri(TrailerLink);
            Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        private void btnGenre_Clicked(object sender, EventArgs e)
        {
            Genre genre = (Genre)((Button)sender).BindingContext;
            //Debug.WriteLine("Genre: " + genre.GenreInfo.Name) ;
            if (genre != null)
            {
                Navigation.PushAsync(new FilteredByGenrePage(genre, "anime"));
            }
        }
    }
}