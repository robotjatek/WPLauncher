
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WPLauncher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TilePage : ContentPage
    {
        public TilePage()
        {
            InitializeComponent();
            var width = MainGrid.Width / 2;

            //var box01 = new BoxView()
            //{
            //    HeightRequest = width,
            //    WidthRequest = width,
            //    BackgroundColor = Color.AliceBlue
            //};

            //MainGrid.Children.Add(box01, 0, 0, 0, 0);
            //MainGrid.Children.Add(box01, 0, 1, 0, 0);
        }
    }
}