
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WPLauncher.Models;
using WPLauncher.Services;

using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace WPLauncher.ViewModels
{
    public class AppListViewModel
    {
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
            var applistOrdered = (await applicationService.GetApplicationList()).OrderBy(a => a.ReadableName);
            var difference = applistOrdered.Union(AppList).Except(applistOrdered.Intersect(AppList));

            if (difference.Any())
            {
                this.AppList.Clear();
                foreach (var app in applistOrdered)
                {
                    this.AppList.Add(app);
                }
            }
        }

        public ICommand RunApplicationCommand => new Command<AppProperties>(OnItemTapped);

        public ICommand LongPressCommand => new AsyncCommand<AppProperties>(OpenContextMenu);

        private async Task OpenContextMenu(AppProperties selected)
        {
            var pinToStartAction = "Pin to start";
            var uninstallAction = "Uninstall";

            var action = await Application.Current.MainPage.DisplayActionSheet($"{selected.ReadableName}", "Cancel", null, new[] { pinToStartAction, uninstallAction, "Application info" });

            if (action == pinToStartAction)
            {
                this.tileService.PinTile(selected);
            }
            else if (action == uninstallAction)
            {
                this.applicationService.UninstallApplication(selected);
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
