using WPLauncher.Models;

using Xamarin.Forms;

namespace WPLauncher.Services
{
    public class SettingsService : ISettingsService
    {
        private Color _accentColor = AccentColors.Cobalt;
        private readonly IApplicationService _applicationService;

        public Color AccentColor
        {
            get => _accentColor;
            set
            {
                _accentColor = value;
                SettingChanged();
            }
        }

        public event SettingChangedEventHandler SettingChanged;

        public SettingsService(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        public void ClearIconCache()
        {
            _applicationService.ClearCache();
        }
    }
}
