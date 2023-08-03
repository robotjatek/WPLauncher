using System.Collections.Generic;
using System.Threading.Tasks;
using WPLauncher.Models;
using WPLauncher.Pages;

using Xamarin.Forms;

namespace WPLauncher.Services
{
    public class LauncherApplicationService : ILauncherApplicationService
    {
        private readonly List<AppProperties> _apps = new List<AppProperties>();

        public LauncherApplicationService(SettingsPage settingsPage)
        {
            var settingsApp = new AppProperties("Launcher Settings", "launcher:settings", ImageSource.FromFile("settings"), new LauncherRunnable(settingsPage));
            _apps.Add(settingsApp);
        }

        public async Task<IEnumerable<AppProperties>> GetLauncherApplications()
        {
            return _apps;
        }
    }
}
