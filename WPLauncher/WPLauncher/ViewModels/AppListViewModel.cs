
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using WPLauncher.Services;

using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace WPLauncher.ViewModels
{
    public class AppListViewModel
    {
        private bool loaded = false; //TODO: what to do when the applist is updated?
        private readonly IApplicationService applicationService;
        private readonly ITileService tileService;

        public ObservableCollection<AppProperties> AppList { get; } = new ObservableCollection<AppProperties>();

        public AppListViewModel(IApplicationService applicationService, ITileService tileService)
        {
            this.applicationService = applicationService;
            this.tileService = tileService;
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
            var pinToStartAction = "Pin to start";

            var action = await Application.Current.MainPage.DisplayActionSheet($"{selected.Name}", "Cancel", null, new[] { pinToStartAction, "Uninstall", "Application info" });

            if (action == pinToStartAction)
            {
                this.tileService.PinTile(selected);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("", action, "Cancel");
            }
        }

        private void OnItemTapped(AppProperties app)
        {
            app.RunApplication();
        }
    }
}
