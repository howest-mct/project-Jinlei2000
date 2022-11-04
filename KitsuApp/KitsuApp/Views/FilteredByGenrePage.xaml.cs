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
    public partial class FilteredByGenrePage : ContentPage
    {
        public Genre genreContent { get; set; }
        public FilteredByGenrePage(Genre genre)
        {
            InitializeComponent();
            this.genreContent = genre;
            Debug.WriteLine(genreContent.GenreInfo.Name);
            // Change Title to genre name
            Title = genreContent.GenreInfo.Name;
        }
    }
}