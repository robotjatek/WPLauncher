
using Xamarin.Forms;

namespace WPLauncher.Pages
{
    public class StartPage : CarouselPage
    {
        public StartPage(TilePage tilePage, AppListPage applistPage)
        {
            this.Children.Add(tilePage);
            this.Children.Add(applistPage);
        }
    }
}
