
using WPLauncher.Pages;

using Xamarin.Forms;

namespace WPLauncher
{
    public partial class App : Application
    {
        public App(StartPage startpage)
        {
            InitializeComponent();
            MainPage = startpage;
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
