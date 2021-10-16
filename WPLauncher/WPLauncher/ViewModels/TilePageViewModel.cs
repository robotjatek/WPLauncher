using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

using WPLauncher.Models;
using WPLauncher.Services;

using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace WPLauncher.ViewModels
{
    public class TilePageViewModel : BaseViewModel
    {
        private ObservableCollection<TileModel> _tileModels;

        public ObservableCollection<TileModel> TileModels
        {
            get => _tileModels;

            private set
            {
                _tileModels = value;
                OnPropertyChanged();
            }
        }

        public ICommand UnpinTileCommand { get; private set; }

        public ICommand OpenContextMenuCommand { get; private set; }

        public ICommand RunApplicationCommand { get; private set; }

        private readonly ITileService tileService;

        public TilePageViewModel(ITileService tileService)
        {
            this.tileService = tileService;
            this.tileService.TileListChanged += TileService_TileListChanged;

            UnpinTileCommand = new Command<TileModel>((toRemove) => RemoveTile(toRemove));
            OpenContextMenuCommand = new AsyncCommand<TileModel>((pressedTile) => OpenContextMenu(pressedTile));
            RunApplicationCommand = new AsyncCommand<TileModel>((pressedTile) => RunApplication(pressedTile));

            TileModels = new ObservableCollection<TileModel>(this.tileService.GetTiles());
        }

        private void TileService_TileListChanged()
        {
            RefreshTiles();
        }

        private async Task RunApplication(TileModel tile)
        {
            await Application.Current.MainPage.DisplayAlert("", $"{tile.Title} started", "Cancel");
        }

        private async Task OpenContextMenu(TileModel pressedTile)
        {
            var action = await Application.Current.MainPage.DisplayActionSheet("", "Cancel", null, new[] { "Unpin" });
            if (string.Equals(action, "unpin", StringComparison.InvariantCultureIgnoreCase))
            {
                RemoveTile(pressedTile);
            }
        }

        private void RemoveTile(TileModel toRemove)
        {
            this.tileService.UnpinTile(toRemove);
        }

        private void RefreshTiles()
        {
            var tiles = this.tileService.GetTiles();
            this.TileModels = new ObservableCollection<TileModel>(tiles);
        }
    }
}
