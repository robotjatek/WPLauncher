using System.Collections.Generic;

using WPLauncher.Models;

namespace WPLauncher.Services
{
    public delegate void TileListChangedEventHandler();

    public interface ITileService
    {
        void PinTile(AppProperties applicationProperties);
        void UnpinTile(TileModel model);
        void UnpinTile(string packageName);
        List<TileModel> Tiles { get; }
        void OnTileDrop(DropEventArgs args, int newColumnPosition, int newRowPosition);

        event TileListChangedEventHandler TileListChanged;
    }
}
