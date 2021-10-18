
using WPLauncher.ViewModels;

using Xamarin.Forms;

namespace WPLauncher
{
    public partial class AppListPage : ContentPage
    {
        private readonly AppListViewModel appListViewModel;

        public AppListPage(AppListViewModel appListViewModel)
        {
            InitializeComponent();
            this.appListViewModel = appListViewModel;
            BindingContext = appListViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await appListViewModel.InitCollection();
        }
    }
}
