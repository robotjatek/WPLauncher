﻿using System;
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

        private readonly ITileService _tileService;
        private readonly ISettingsService _settingsService;

        public TilePageViewModel(ITileService tileService, ISettingsService settingsService)
        {
            _settingsService = settingsService;
            _settingsService.SettingChanged += SettingsService_SettingChanged;

            _tileService = tileService;
            _tileService.TileListChanged += TileService_TileListChanged;

            UnpinTileCommand = new Command<TileModel>((toRemove) => RemoveTile(toRemove));
            OpenContextMenuCommand = new AsyncCommand<TileModel>((pressedTile) => OpenContextMenu(pressedTile));
            RunApplicationCommand = new AsyncCommand<TileModel>((pressedTile) => RunApplication(pressedTile));

            TileModels = new ObservableCollection<TileModel>(_tileService.GetTiles());
            TileColor = _settingsService.AccentColor;
        }

        private void SettingsService_SettingChanged(string settingName)
        {
            if(settingName.Equals("AccentColor", StringComparison.InvariantCultureIgnoreCase))
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
            await Task.Run(() => tile.AppProperties.RunApplication());
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
            this._tileService.UnpinTile(toRemove);
        }

        private void RefreshTiles()
        {
            var tiles = _tileService.GetTiles();
            TileModels = new ObservableCollection<TileModel>(tiles);
        }
    }
}
