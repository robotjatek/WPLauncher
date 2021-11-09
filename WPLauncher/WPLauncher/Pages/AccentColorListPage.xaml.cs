using WPLauncher.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WPLauncher.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccentColorListPage : ContentPage
    {
        private readonly SettingsPageViewModel _vm;

        public AccentColorListPage(SettingsPageViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            this.BindingContext = vm;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await _vm.PopView();
        }
    }
}