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
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //change the color of the selected tab
            Shell.SetTabBarTitleColor(this, Color.FromHex("#ffffff"));

            //change the color of the tab bar
            Shell.SetTabBarBackgroundColor(this, Color.FromHex("#6677F6"));

            //change the color of the unselected tab
            Shell.SetUnselectedColor(this, Color.FromHex("#C5CCFB"));
        }
    }
}