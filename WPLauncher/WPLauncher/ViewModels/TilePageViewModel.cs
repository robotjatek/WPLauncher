using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

using WPLauncher.Models;
using WPLauncher.Services;

using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

// TODO: animate empty space removal
// TODO: animate tile rearrange
// TODO: restore enable/disable rearrange mode
// TODO: restore carousel view
// TODO: restore scroll view
// Old WP7 UI demo: https://www.youtube.com/watch?v=RGaAiHihnyc
namespace WPLauncher.ViewModels
{
    public class TilePageViewModel : BaseViewModel
    {
        private BindingList<TileModel> _tileModels;

        public TilePage TilePageRef { get; set; }

        public TileModel SelectedTile { get; private set; }

        public BindingList<TileModel> TileModels
        {
            get => _tileModels;

            private set
            {
                _tileModels = value;
                OnPropertyChanged();
            }
        }

        private Color _tileColor;
        public Color TileColor
        {
            get => _tileColor;

            private set
            {
                _tileColor = value;
                OnPropertyChanged();
            }
        }

        public ICommand UnpinTileCommand { get; private set; }

        public ICommand OpenContextMenuCommand { get; private set; }

        public ICommand RunApplicationCommand { get; private set; }

        public ICommand CancelRearrangeCommand { get; private set; }

        public ICommand OnDropCommand { get; private set; }

        public ICommand OnDragCommand { get; private set; }

        private readonly ITileService _tileService;
        private readonly ISettingsService _settingsService;

        public TilePageViewModel(ITileService tileService, ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _settingsService.SettingChanged += SettingsService_SettingChanged;

            _tileService = tileService;
            _tileService.TileListChanged += TileService_TileListChanged;

            UnpinTileCommand = new Command<TileModel>(RemoveTile);
            OpenContextMenuCommand = new AsyncCommand<TileModel>(OpenContextMenu);
            RunApplicationCommand = new AsyncCommand<TileModel>(RunApplication);
            CancelRearrangeCommand = new Command(CancelRearrangeMode);
            OnDropCommand = new Command<DropEventArgs>(OnDropTile);
            OnDragCommand = new Command<DropEventArgs>(OnDragTile);

            TileModels = new BindingList<TileModel>(_tileService.Tiles);
            TileColor = _settingsService.AccentColor;
        }

        private void SettingsService_SettingChanged(string settingName)
        {
            if (settingName.Equals("AccentColor", StringComparison.InvariantCultureIgnoreCase))
            {
                TileColor = _settingsService.AccentColor;
            }
        }

        private void TileService_TileListChanged()
        {
            RefreshTiles();
        }

        private async Task RunApplication(TileModel tile)
        {
            if (SelectedTile == null)
            {
                await Task.Run(() => tile.AppProperties?.RunApplication());
            }
            else
            {
                SelectTileToRearrange(tile);
            }
        }

        private async Task OpenContextMenu(TileModel pressedTile)
        {
            //if (SelectedTile == null)
            //{
            //    var action = (await Application.Current.MainPage.DisplayActionSheet("", "Cancel", null, new[] { "Unpin", "Rearrange" })).ToUpperInvariant();
            //    switch (action)
            //    {
            //        case "UNPIN":
            //            RemoveTile(pressedTile);
            //            break;
            //        case "REARRANGE":
            //            SelectTileToRearrange(pressedTile);
            //            break;
            //    }
            //}
        }

        private void SelectTileToRearrange(TileModel tile)
        {
            if (tile == SelectedTile)
            {
                CancelRearrangeMode();
            }
            else
            {
                CancelRearrangeMode();
                SelectedTile = tile;
                tile.Scale = 0.8;
            }
        }

        private void CancelRearrangeMode()
        {
            if (SelectedTile != null)
            {
                SelectedTile.Scale = 1.0;
            }
            SelectedTile = null;
        }

        private void RemoveTile(TileModel toRemove)
        {
            _tileService.UnpinTile(toRemove);
        }

        private void OnDropTile(DropEventArgs args)
        {
            var (column, row) = CalculateNewPosition(args);
            if (IsInBounds(args, column, row))
            {
                _tileService.OnTileDrop(args, column, row);
            }
            TilePageRef.HideDropTarget();
        }

        private void OnDragTile(DropEventArgs args)
        {
            var (column, row) = CalculateNewPosition(args);
            if (IsInBounds(args, column, row))
            {
                TilePageRef.ShowDropTarget(column, row, args.TileModel);
            }
            else
            {
                // Show old position for the droptarget if the new position is invalid
                TilePageRef.ShowDropTarget(
                    args.TileModel.Position.Column,
                    args.TileModel.Position.Row,
                    args.TileModel);
            }
        }

        private (int column, int row) CalculateNewPosition(DropEventArgs args)
        {
            // TODO: lefelé "túldobni" a tilet

            var gridCellWidth = TilePageRef.Width / 4;

            var calculatedTranslationX = args.TranslationX / gridCellWidth;
            var calculatedTranslationY = args.TranslationY / gridCellWidth;

            var calculatedColumn = (int)Math.Round(args.TileModel.Position.Column + calculatedTranslationX);
            var calculatedRow = (int)Math.Round(args.TileModel.Position.Row + calculatedTranslationY);

            return (calculatedColumn, calculatedRow);
        }

        private static bool IsInBounds(DropEventArgs args, int column, int row)
        {
            return column >= 0 && column + args.TileModel.Size.Width <= 4 && row >= 0;
        }

        private void RefreshTiles()
        {
            TileModels = new BindingList<TileModel>(_tileService.Tiles);
        }
    }
}
