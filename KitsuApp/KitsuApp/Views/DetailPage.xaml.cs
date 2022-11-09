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
                lblName.Text = anime.AnimeInfo.Name;
                lblRating.Text = anime.Rating.ToString();
                imgPoster.Source = anime.AnimeInfo.PosterImage.Medium;
            }
            else if (CollectionContent.CollectionType == "manga")
            {
                Manga manga = (Manga)CollectionContent;
                Debug.WriteLine("ShowDetail: " + manga.MangaInfo.Slug);
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
    }
}