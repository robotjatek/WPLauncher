
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WPLauncher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedView : TabbedPage
    {
        public TabbedView(AppListPage mainPage, TilePage tilePage, TilePage2 tilePage2)
        {
            InitializeComponent();
            this.Children.Add(mainPage);
            this.Children.Add(tilePage);
            this.Children.Add(tilePage2);
        }
    }
}