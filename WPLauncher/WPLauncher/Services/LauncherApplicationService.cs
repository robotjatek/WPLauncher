using System.Collections.Generic;
using System.Threading.Tasks;

using WPLauncher.Pages;

using Xamarin.Forms;

namespace WPLauncher.Services
{
    public class LauncherApplicationService : ILauncherApplicationService
    {
        private readonly SettingsPage _settingsPage;

        public LauncherApplicationService(SettingsPage settingsPage)
        {
            _settingsPage = settingsPage;
        }

        public async Task<IEnumerable<AppProperties>> GetLauncherApplications()
        {
            var app = new AppProperties("Launcher Settings", "launcher:settings", ImageSource.FromFile("robot"), new LauncherRunnable(_settingsPage));
            return new[] { app };
        }
    }
}
