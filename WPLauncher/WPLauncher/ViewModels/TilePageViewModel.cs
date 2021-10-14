using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

using WPLauncher.Models;

using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

using static WPLauncher.TileSizeDefinitions;

namespace WPLauncher.ViewModels
{
    public class TilePageViewModel
    {
        public ObservableCollection<TileModel> TileModels { get; } = new ObservableCollection<TileModel>();

        public ICommand UnpinTileCommand { get; private set; }

        public ICommand OpenContextMenuCommand { get; private set; }

        public ICommand RunApplicationCommand { get; private set; }

        private readonly TileSizeDefinitions tileSizeDefinitions = new TileSizeDefinitions();

        public TilePageViewModel()
        {
            var tiles = new TileModel[]
            {
                CreateTile(Guid.NewGuid().ToString().Substring(0,10), TileSizeMode.Medium, new Position{ Row = 0, Column = 0 }, Color.Green),
                CreateTile(Guid.NewGuid().ToString().Substring(0,10), TileSizeMode.Medium, new Position{ Row = 0, Column = 2 }, Color.Red),
                CreateTile(Guid.NewGuid().ToString().Substring(0,10), TileSizeMode.Medium, new Position{ Row = 2, Column = 0 }, Color.Red),
                CreateTile(Guid.NewGuid().ToString().Substring(0,10), TileSizeMode.Medium, new Position{ Row = 2, Column = 2 }, Color.Green),
                CreateTile(Guid.NewGuid().ToString().Substring(0,10), TileSizeMode.Medium, new Position{ Row = 4, Column = 0 }, Color.Red),
                CreateTile(Guid.NewGuid().ToString().Substring(0,10), TileSizeMode.Medium, new Position{ Row = 4, Column = 2 }, Color.Green),
                CreateTile(Guid.NewGuid().ToString().Substring(0,10), TileSizeMode.Medium, new Position{ Row = 6, Column = 0 }, Color.Red),
                CreateTile(Guid.NewGuid().ToString().Substring(0,10), TileSizeMode.Medium, new Position{ Row = 6, Column = 2 }, Color.Green),

                CreateTile(Guid.NewGuid().ToString().Substring(0,10), TileSizeMode.Small, new Position{ Row = 8, Column = 0 }, Color.Blue),
                CreateTile(Guid.NewGuid().ToString().Substring(0,10), TileSizeMode.Small, new Position{ Row = 8, Column = 1 }, Color.Blue),
                CreateTile(Guid.NewGuid().ToString().Substring(0,10), TileSizeMode.Small, new Position{ Row = 8, Column = 2 }, Color.Blue),
                CreateTile(Guid.NewGuid().ToString().Substring(0,10), TileSizeMode.Small, new Position{ Row = 8, Column = 3 }, Color.Blue),

                CreateTile(Guid.NewGuid().ToString().Substring(0,10), TileSizeMode.Large, new Position{ Row = 9, Column = 0 }, Color.YellowGreen),

                CreateTile("Wide tile", TileSizeMode.Wide, new Position{ Row = 13, Column = 0 }, Color.Coral),

                CreateTile(Guid.NewGuid().ToString().Substring(0,10), TileSizeMode.Medium, new Position{ Row = 15, Column = 1 }, Color.FromHex("1BA1E2")),

                CreateTile(Guid.NewGuid().ToString().Substring(0,10), TileSizeMode.Small, new Position{ Row = 15, Column = 0 }, Color.Blue),
                CreateTile(Guid.NewGuid().ToString().Substring(0,10), TileSizeMode.Small, new Position{ Row = 16, Column = 0 }, Color.Blue),
                CreateTile(Guid.NewGuid().ToString().Substring(0,10), TileSizeMode.Small, new Position{ Row = 15, Column = 3 }, Color.Blue),
                CreateTile(Guid.NewGuid().ToString().Substring(0,10), TileSizeMode.Small, new Position{ Row = 16, Column = 3 }, Color.Blue),

            };

            foreach (var tile in tiles)
            {
                TileModels.Add(tile);
            }

            UnpinTileCommand = new Command<TileModel>((toRemove) => RemoveTile(toRemove));
            OpenContextMenuCommand = new AsyncCommand<TileModel>((pressedTile) => OpenContextMenu(pressedTile));
            RunApplicationCommand = new AsyncCommand<TileModel>((pressedTile) => RunApplication(pressedTile));
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
            this.TileModels.Remove(toRemove);
        }

        private TileModel CreateTile(string title, TileSizeMode sizeMode, Position position, Color color)
        {
            var size = this.tileSizeDefinitions.GetTileSize(sizeMode);
            return new TileModel
            {
                Title = title,
                Size = size,
                Position = position,
                Color = color
            };
        }
    }
}
