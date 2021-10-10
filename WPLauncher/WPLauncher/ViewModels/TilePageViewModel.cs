using System;
using System.Collections.ObjectModel;

using WPLauncher.Models;

using Xamarin.Forms;

using static WPLauncher.TileSizeDefinitions;

namespace WPLauncher.ViewModels
{
    public class TilePageViewModel
    {
        public ObservableCollection<TileModel> TileModels { get; set; } = new ObservableCollection<TileModel>();
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
