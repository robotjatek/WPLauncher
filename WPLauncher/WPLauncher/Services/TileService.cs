using System.Collections.Generic;
using System.Linq;

using WPLauncher.Models;

using static WPLauncher.TileSizeDefinitions;

namespace WPLauncher.Services
{
    public class TileService : ITileService
    {
        private readonly List<TileModel> _tiles = new List<TileModel>();
        private readonly TileSizeDefinitions tileSizeDefinitions = new TileSizeDefinitions();

        public event TileListChangedEventHandler TileListChanged;

        public TileService()
        {
            _tiles.Add(CreateTile("q", TileSizeMode.Medium, new Position { Row = 0, Column = 0 }, null));
            _tiles.Add(CreateTile("asd", TileSizeMode.Medium, new Position { Row = 0, Column = 2 }, null));
            _tiles.Add(CreateTile("Second", TileSizeMode.Medium, new Position { Row = 2, Column = 0 }, null));
            _tiles.Add(CreateTile("Third", TileSizeMode.Medium, new Position { Row = 4, Column = 0 }, null));
        }

        public void PinTile(AppProperties applicationProperties)
        {
            //TODO: calculate tile positions dynamically
            var lowestPoint = _tiles.DefaultIfEmpty(new TileModel()).Max(t => t.Position.Row + t.Size.Height);
            if (_tiles.Count % 2 != 0)
            {
                lowestPoint -= 2;
            }
            var tile = CreateTile(applicationProperties.ReadableName, TileSizeMode.Medium, new Position { Row = lowestPoint, Column = _tiles.Count % 2 * 2 }, applicationProperties);

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

        public List<TileModel> GetTiles()
        {
            return _tiles;
        }

        public void OnTileDrop(DropEventArgs args, int newColumnPosition, int newRowPosition)
        {
            args.TileModel.Position = new Position
            {
                Row = newRowPosition,
                Column = newColumnPosition
            };
            // Note this mustn't call the TileListChanged event as the list itself didn't change only the stored element
        }

        private TileModel CreateTile(string title, TileSizeMode sizeMode, Position position, AppProperties app)
        {
            var size = this.tileSizeDefinitions.GetTileSize(sizeMode);
            return new TileModel
            {
                Title = title,
                Size = size,
                Position = position,
                AppProperties = app,
            };
        }
    }
}
