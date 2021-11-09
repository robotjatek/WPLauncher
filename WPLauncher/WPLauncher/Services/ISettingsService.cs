using System.Runtime.CompilerServices;

using Xamarin.Forms;

namespace WPLauncher.Services
{
    public delegate void SettingChangedEventHandler([CallerMemberName]string settingName = "");

    public interface ISettingsService
    {
        Color AccentColor { get; set; }

        event SettingChangedEventHandler SettingChanged;
    }
}
