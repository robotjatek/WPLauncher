
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WPLauncher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedView : TabbedPage
    {
        public TabbedView(AppListPage mainPage, TilePage tilePage)
        {
            InitializeComponent(); //TODO: use carousel view instead of tabbedpage
            this.Children.Add(tilePage);
            this.Children.Add(mainPage);
        }
    }
}