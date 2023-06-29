
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WPLauncher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WideTile : ContentView
    {
        public WideTile()
        {
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, width / 2);
            HeightRequest = width / 2;
        }
    }

    // TODO: support drag and drop
}