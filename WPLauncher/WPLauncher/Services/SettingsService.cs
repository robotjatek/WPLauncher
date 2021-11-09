using WPLauncher.Models;

using Xamarin.Forms;

namespace WPLauncher.Services
{
    public class SettingsService : ISettingsService
    {
        private Color _accentColor = AccentColors.Cobalt;

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
    }
}
