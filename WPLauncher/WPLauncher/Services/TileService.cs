using System;
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

        public List<TileModel> Tiles { get { return _tiles; } }

        // TODO: Use MediatR instead of events
        public event TileListChangedEventHandler TileListChanged;

        public TileService()
        {
            _tiles.Add(CreateTile("q", TileSizeMode.Medium, new Position { Row = 0, Column = 0 }, null));
            _tiles.Add(CreateTile("asd", TileSizeMode.Medium, new Position { Row = 1, Column = 2 }, null));
            _tiles.Add(CreateTile("Second", TileSizeMode.Medium, new Position { Row = 2, Column = 0 }, null));
            _tiles.Add(CreateTile("Third", TileSizeMode.Wide, new Position { Row = 6, Column = 0 }, null));
        }

        public void PinTile(AppProperties applicationProperties)
        {
            //TODO: calculate tile positions dynamically - last line first available position or in a line first position
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

        // This is called when a pinned application is uninstalled
        public void UnpinTile(string packageName)
        {
            var tile = _tiles.FirstOrDefault(t => t.AppProperties.PackageName == packageName);
            if (tile != null)
            {
                UnpinTile(tile);
            }
        }

        public void OnTileDrop(DropEventArgs args, int newColumnPosition, int newRowPosition)
        {
            HandleTileCollisions(args, newColumnPosition, newRowPosition);

            args.TileModel.Position = new Position
            {
                Row = newRowPosition,
                Column = newColumnPosition
            };
            // Note this mustn't call the TileListChanged event as the list itself didn't change only the stored element
        }

        private void HandleTileCollisions(DropEventArgs args, int newColumnPosition, int newRowPosition)
        {
            var collisions = CheckCollisionsForNewCoordinates(newColumnPosition, newRowPosition, args.TileModel);
            if (collisions.Any())
            {
                var lowestPoint = LowestRowInGroup(collisions);
                var offset = MoveCollidedTilesToNewPositions(args, newRowPosition, collisions);
                MoveTilesBelowTheCollidedTilesByOffset(args, collisions, lowestPoint, offset);
            }
        }

        private void MoveTilesBelowTheCollidedTilesByOffset(DropEventArgs args, IEnumerable<TileModel> collisions, int lowestPoint, int maxOffset)
        {
            var tilesBelow = GetTilesBelowCollidedTiles(args, collisions, lowestPoint);
            foreach (var tile in tilesBelow)
            {
                tile.Position = new Position
                {
                    Row = tile.Position.Row + maxOffset,
                    Column = tile.Position.Column
                };
            }
        }

        private int MoveCollidedTilesToNewPositions(DropEventArgs args, int newRowPosition, IEnumerable<TileModel> collisions)
        {
            var maxOffset = collisions.Max(tile => CalculateXOffset(tile, newRowPosition, args.TileModel.Size.Height));
            foreach (var item in collisions)
            {
                item.Position = new Position
                {
                    Row = item.Position.Row + maxOffset,
                    Column = item.Position.Column
                };
            }

            return maxOffset;
        }

        private IEnumerable<TileModel> GetTilesBelowCollidedTiles(DropEventArgs args, IEnumerable<TileModel> collisions, int lowestPoint)
        {
            var notCollidedTiles = _tiles.Except(collisions).ToList();
            notCollidedTiles.Remove(args.TileModel);
            var tilesBelow = notCollidedTiles.Where(tile => tile.Position.Row >= lowestPoint);
            return tilesBelow;
        }

        private int CalculateXOffset(TileModel tile, int tile2Row, int tile2Height)
        {
            var newPosX = tile2Row + tile2Height;
            return newPosX - tile.Position.Row; // new position - old position
        }

        private int LowestRowInGroup(IEnumerable<TileModel> tiles)
        {
            return tiles.Max(tile => tile.Position.Row + tile.Size.Height);
        }

        private IEnumerable<TileModel> CheckCollisionsForNewCoordinates(int column, int row, TileModel tileModel)
        {
            // Note: tilemodel has the OLD coordinates here so only its reference and size values are relevant here
            var collidedTiles = new List<TileModel>();

            foreach (var tile in _tiles)
            {
                if (tile != tileModel)
                {
                    var collisionX = tile.Position.Column < column + tileModel.Size.Width &&
                        tile.Position.Column + tile.Size.Width > column;

                    var collisionY = tile.Position.Row < row + tileModel.Size.Height &&
                        tile.Position.Row + tile.Size.Height > row;

                    if (collisionX && collisionY)
                    {
                        collidedTiles.Add(tile);
                    }
                }
            }
            return collidedTiles;
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
