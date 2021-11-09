using WPLauncher.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WPLauncher.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private readonly SettingsPageViewModel _vm;
        public SettingsPage(SettingsPageViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            BindingContext = _vm;
        }

        protected override void OnAppearing()
        {
            _vm.InitPage();
        }
    }
}