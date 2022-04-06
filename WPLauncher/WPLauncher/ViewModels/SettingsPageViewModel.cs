using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using WPLauncher.Models;
using WPLauncher.Pages;
using WPLauncher.Services;

using Xamarin.Forms;

namespace WPLauncher.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        public AccentColorListPage _accentColorPage { get; set; }
        public ISettingsService _settingsService { get; set; }

        private NamedColor _selectedColor;

        public NamedColor SelectedColor
        {
            get => _selectedColor;
            set
            {
                _selectedColor = value;
                if (value != null)
                {
                    _settingsService.AccentColor = value.Color;
                }
                OnPropertyChanged();
            }
        }

        public ICommand AccentColorSelectionTapped { get; private set; }
        public ICommand ClearIconCacheCommand { get; private set; }

        public ObservableCollection<NamedColor> AccentColorList { get; private set; } = new ObservableCollection<NamedColor>(NamedColor.NamedColors);

        public SettingsPageViewModel()
        {
            AccentColorSelectionTapped = new Command(async () =>
            {
                await OpenAccentColorListPage();
            });

            ClearIconCacheCommand = new Command(() =>
            {
                _settingsService.ClearIconCache();
            });
        }

        public void InitPage()
        {
            SelectedColor = AccentColorList.First(c => c.Color == _settingsService.AccentColor);
        }

        public async Task PopView()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async Task OpenAccentColorListPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(_accentColorPage);
        }
    }
}