using KitsuApp.Models;
using KitsuApp.Repositories;
using KitsuApp.Services;
using KitsuApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KitsuApp
{
    public partial class MainPage : Shell
    {
        public MainPage()
        {
            InitializeComponent();
            //change the color of the selected tab
            Shell.SetTabBarTitleColor(this, Color.FromHex("#ffffff"));

            //change the color of the tab bar
            Shell.SetTabBarBackgroundColor(this, Color.FromHex("#131949"));

            //change the color of the unselected tab
            Shell.SetUnselectedColor(this, Color.FromHex("#C5CCFB"));
        }



    }
}
