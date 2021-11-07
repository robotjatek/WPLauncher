
using Xamarin.Forms;

namespace WPLauncher.Pages
{
    public class StartPage : CarouselPage
    {
        public StartPage(TilePage tilePage, AppListPage applistPage)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            this.Children.Add(tilePage);
            this.Children.Add(applistPage);
        }
    }
}
