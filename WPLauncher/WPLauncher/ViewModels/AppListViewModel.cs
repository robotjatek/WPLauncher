
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace WPLauncher.ViewModels
{
    public class AppListViewModel : BindableObject
    {
        private bool loaded = false; //TODO: what to do when the applist is updated?
        private readonly IApplicationService applicationService;

        public ObservableCollection<AppProperties> AppList { get; } = new ObservableCollection<AppProperties>();

        public AppListViewModel(IApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }

        public async Task InitCollection()
        {
            if (!loaded)
            {
                var applistOrdered = (await applicationService.GetApplicationList()).OrderBy(a => a.Name);
                foreach (var app in applistOrdered)
                {
                    this.AppList.Add(app);
                }
                loaded = true;
            }
        }

        public ICommand RunApplicationCommand => new Command<AppProperties>(OnItemTapped);

        public ICommand LongPressCommand => new AsyncCommand<AppProperties>(OpenContextMenu);

        private async Task OpenContextMenu(AppProperties selected)
        {
            var action = await Application.Current.MainPage.DisplayActionSheet($"{selected.Name}", "Cancel", null, new[] { "Pin to start", "Uninstall", "Application info" });
            await Application.Current.MainPage.DisplayAlert("", action, "Cancel");
        }

        private void OnItemTapped(AppProperties app)
        {
            app.RunApplication();
        }
    }
}
