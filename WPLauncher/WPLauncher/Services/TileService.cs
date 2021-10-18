using System.Collections.Generic;
using System.Linq;

using WPLauncher.Models;

using Xamarin.Forms;

using static WPLauncher.TileSizeDefinitions;

namespace WPLauncher.Services
{
    public class TileService : ITileService
    {
        private readonly List<TileModel> _tiles = new List<TileModel>();
        private readonly TileSizeDefinitions tileSizeDefinitions = new TileSizeDefinitions();

        public event TileListChangedEventHandler TileListChanged;

        public void PinTile(AppProperties applicationProperties)
        {
            //TODO: calculate tile positions dynamically
            var lowestPoint = _tiles.DefaultIfEmpty(new TileModel()).Max(t => t.Position.Row + t.Size.Height);
            var tile = CreateTile(applicationProperties.ReadableName, TileSizeMode.Medium, new Position { Row = lowestPoint, Column = 0 }, Color.Gold, applicationProperties);

            _tiles.Add(tile);
            TileListChanged();
        }

        public void UnpinTile(TileModel tile)
        {
            _tiles.Remove(tile);
            TileListChanged();
        }

        public void UnpinTile(string packageName)
        {
            var tile = _tiles.FirstOrDefault(t => t.AppProperties.PackageName == packageName);
            if (tile != null)
            {
                UnpinTile(tile);
            }
        }

        public IEnumerable<TileModel> GetTiles()
        {
            return _tiles;
        }

        private TileModel CreateTile(string title, TileSizeMode sizeMode, Position position, Color color, AppProperties app)
        {
            var size = this.tileSizeDefinitions.GetTileSize(sizeMode);
            return new TileModel
            {
                Title = title,
                Size = size,
                Position = position,
                Color = color,
                AppProperties = app,
            };
        }
    }
}
