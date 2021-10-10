using Xamarin.Forms;

using static WPLauncher.TileSizeDefinitions;

namespace WPLauncher.Models
{
    public class Position
    {
        public int Column { get; set; }

        public int Row { get; set; }
    }

    public class TileModel
    {
        public Color Color { get; set; }

        public string Title { get; set; }

        public TileSize Size { get; set; }

        public Position Position { get; set; }
    }
}
