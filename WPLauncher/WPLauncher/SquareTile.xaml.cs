
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WPLauncher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SquareTile : ContentView
    {
        public SquareTile()
        {
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, width);
            HeightRequest = width;
        }
    }
}