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

        private NamedColor _selectedColor;
        private readonly ISettingsService _settingsService;

        public NamedColor SelectedColor
        {
            get
            {
                return _selectedColor;
            }
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

        public ObservableCollection<NamedColor> AccentColorList { get; private set; } = new ObservableCollection<NamedColor>(NamedColor.NamedColors);

        public SettingsPageViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;

            AccentColorSelectionTapped = new Command(async () =>
            {
                await OpenAccentColorListPage();
            });
        }

        public void InitPage()
        {
            SelectedColor = AccentColorList.Where(c => c.Color == _settingsService.AccentColor).First();
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