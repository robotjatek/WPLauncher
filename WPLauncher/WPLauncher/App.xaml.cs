
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

            MainPage = new TabbedView(applistPage, tilePage);
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
