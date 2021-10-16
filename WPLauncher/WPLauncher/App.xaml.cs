
using WPLauncher.ViewModels;

using Xamarin.Forms;

namespace WPLauncher
{
    public partial class App : Application
    {
        public App(AppListViewModel appListViewModel, TilePageViewModel tilePageViewModel)
        {
            InitializeComponent();
            var applistPage = new AppListPage(appListViewModel)
            {
                Title = "App list"
            };
            var tilePage = new TilePage(tilePageViewModel)
            {
                Title = "Grid"
            };
            var tilePage2 = new TilePage2()
            {
                Title = "StackLayout"
            };
            MainPage = new TabbedView(applistPage, tilePage, tilePage2);
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
