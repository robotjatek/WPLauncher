
using Xamarin.Forms;

namespace WPLauncher
{
    public partial class App : Application
    {
        public App(TilePage startpage, AppListPage applist)
        {
            InitializeComponent();

            //Routing.RegisterRoute(nameof(startpage), typeof(TilePage));
            //Routing.RegisterRoute(nameof(applist), typeof(AppListPage));
            // TODO: MainPage needs to be new AppShell in MAUI
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
