using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WPLauncher.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StaticIcon : ContentView
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(StaticIcon));

        public string Title
        {
            get
            {
                return GetValue(TitleProperty) as string;
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public StaticIcon()
        {
            InitializeComponent();
        }
    }
}