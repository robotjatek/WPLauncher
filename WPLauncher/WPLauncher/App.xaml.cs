
using WPLauncher.Pages;

using Xamarin.Forms;

namespace WPLauncher
{
    public partial class App : Application
    {
        public App(TilePage startpage)
        {
            InitializeComponent();
            MainPage = new NavigationPage(startpage);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
