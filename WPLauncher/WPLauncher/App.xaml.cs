
using Xamarin.Forms;

namespace WPLauncher
{
    public partial class App : Application
    {
        public App(IApplicationService appService)
        {
            InitializeComponent();
            var applistPage = new AppListPage(appService)
            {
                Title = "App list"
            };
            var tilePage = new TilePage()
            {
                Title = "Grid"
            };
            var tilePage2 = new TilePage2()
            {
                Title = "StackLayout"
            };
            MainPage = new TabbedView(applistPage, tilePage, tilePage2);
            //MainPage = new MainPage(appService);
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
