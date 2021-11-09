using WPLauncher.Models;

using Xamarin.Forms;

namespace WPLauncher.Services
{
    public class SettingsService : ISettingsService
    {
        public Color AccentColor { get; set; } = AccentColors.Cobalt;  //TODO: change already pinned tile colors when this setting changes
    }
}
