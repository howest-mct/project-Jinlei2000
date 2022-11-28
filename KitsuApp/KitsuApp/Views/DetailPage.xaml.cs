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
using KitsuApp.Services;

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

        // OnAppearing
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // ConnectivityTest Class
            new ConnectivityTest();
        }
        
        private async Task ShowDetail()
        {
            if (CollectionContent.CollectionType == "anime")
            {
                Anime anime = (Anime)CollectionContent;
                Debug.WriteLine("ShowDetail: " + anime.AnimeInfo.Name);

                // Standard Info & image
                lblName.Text = anime.AnimeInfo.Name;
                lblRating.Text = anime.AnimeInfo.RatingProcent.ToString();
                imgPoster.Source = anime.AnimeInfo.PosterImage.Medium;
                lblTotalTime.Text = anime.AnimeInfo.TotalTime;
                lblEpisodes.Text = anime.AnimeInfo.EpisodeCountString;
                lblDuration.Text = anime.AnimeInfo.EpisodeLengthString;
                lblType.Text = anime.AnimeInfo.Subtype;
                lblStatus.Text = anime.AnimeInfo.StatusString;
                lblAired.Text = anime.AnimeInfo.AiredDate;
                lblSynopsis.Text = anime.AnimeInfo.SynopsisText;

                //More Info
                lblSeason.Text = anime.AnimeInfo.Season;
                lblHighestRatedRank.Text = anime.AnimeInfo.HighestRatedRankString;
                lblPopularityRank.Text = anime.AnimeInfo.PopularityRankString;
                lblMembers.Text = anime.AnimeInfo.MemberCountString;
                lblAgeRating.Text = anime.AnimeInfo.AgeRatingString;
                
                // Trailer
                if (anime.AnimeInfo.TrailerLink == "N/A")
                {
                    btnTrailer.IsVisible = false;
                }
                TrailerLink = anime.AnimeInfo.TrailerLink;

                // Genres
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

                // Characters
                List<Character> characters = await KitsuRepository.GetCharactersFromAnimeOrMangaIDAsync("anime",anime.Id);
                if (characters.Count == 0) {
                    flexCharacters.IsVisible = false;
                    cvwCharacters.IsVisible = false;
                }
                else
                {
                    cvwCharacters.ItemsSource = characters;
                }

                // Episodes
                List<Episode> episodes = await KitsuRepository.GetEpisodesFromAnimeIDAsync(anime.Id);
                if (episodes.Count == 0)
                {
                    cvwEpisodes.IsVisible = false;
                    lblEpisodes.IsVisible = false;
                }
                else
                {
                    cvwEpisodes.ItemsSource = episodes;
                }

            }
            else if (CollectionContent.CollectionType == "manga")
            {
                Manga manga = (Manga)CollectionContent;
                Debug.WriteLine("ShowDetail: " + manga.MangaInfo.Slug);


                // Standard Info & image
                lblName.Text = manga.MangaInfo.Name;
                lblRating.Text = manga.MangaInfo.RatingProcent.ToString();
                imgPoster.Source = manga.MangaInfo.PosterImage.Medium;
                
                lblTotalTimeText.Text = "TOTAL CHAPTERS";
                lblTotalTime.Text = manga.MangaInfo.TotalPages;

                lblEpisodesText.Text = "VOLUME";
                lblEpisodes.Text = manga.MangaInfo.VolumeCountString;

                lblDurationText.Text = "CHAPTER";
                lblDuration.Text = manga.MangaInfo.ChapterCountString;

                lblType.Text = manga.MangaInfo.Subtype;
                lblStatus.Text = manga.MangaInfo.StatusString;
                lblAired.Text = manga.MangaInfo.AiredDate;
                lblSynopsis.Text = manga.MangaInfo.SynopsisText;

                //More Info
                lblSeason.Text = manga.MangaInfo.Season;
                lblHighestRatedRank.Text = manga.MangaInfo.HighestRatedRankString;
                lblPopularityRank.Text = manga.MangaInfo.PopularityRankString;
                lblMembers.Text = manga.MangaInfo.MemberCountString;
                lblAgeRating.Text = manga.MangaInfo.AgeRatingString;

                // Trailer
                btnTrailer.IsVisible = false;
                
                // Genres
                List<Genre> genres = await KitsuRepository.GetGenresFromMangaIDAsync(manga.Id);
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

                // Characters
                List<Character> characters = await KitsuRepository.GetCharactersFromAnimeOrMangaIDAsync("manga", manga.Id);
                if (characters.Count == 0)
                {
                    flexCharacters.IsVisible = false;
                    cvwCharacters.IsVisible = false;
                }
                else
                {
                    cvwCharacters.ItemsSource = characters;
                }

                // Episodes
                cvwEpisodes.IsVisible = false;
                lblEpisodes.IsVisible = false;

            }
        }

        // Draw the progress circle of the rating
        private void CanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            float degree = 0;
            if (CollectionContent.CollectionType == "anime")
            {
                Anime anime = (Anime)CollectionContent;
                degree = anime.AnimeInfo.RatingProcent != 0 ? anime.AnimeInfo.RatingProcent * (360 / 10) : 0;
            }
            else if (CollectionContent.CollectionType == "manga")
            {
                Manga manga = (Manga)CollectionContent;
                degree = manga.MangaInfo.RatingProcent != 0 ? manga.MangaInfo.RatingProcent * (360 / 10) : 0;
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

        // Open trailer link on browser
        private void BtnTrailerClicked(object sender, EventArgs e)
        {
            try
            {
                Uri uri = new Uri(TrailerLink);
                Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                // An unexpected error occured. No browser may be installed on the device.
            }
        }

        // Go to the FilteredByGenrePage
        private void BtnGenreClicked(object sender, EventArgs e)
        {
            Genre genre = (Genre)((Button)sender).BindingContext;
            if (genre != null)
            {
                if(CollectionContent.CollectionType == "anime")
                {
                    Navigation.PushAsync(new FilteredByGenrePage(genre, "anime"));
                }
                else
                {
                    Navigation.PushAsync(new FilteredByGenrePage(genre, "manga"));
                }
            }
        }

        // Add anime to favorites if not already in favorites
        private async void AddToFavorite(object sender, EventArgs e)
        {
            Debug.WriteLine("Button Add_To_Favorite");

            if (CollectionContent.CollectionType == "anime")
            {
                Anime anime = (Anime)CollectionContent;

                bool Check = await KitsuRepository.GetCheckFavNotExists("anime", CollectionContent.Id);
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
            else if (CollectionContent.CollectionType == "manga")
            {
                Manga manga = (Manga)CollectionContent;

                bool Check = await KitsuRepository.GetCheckFavNotExists("manga", CollectionContent.Id);
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

           
        }
    }
}