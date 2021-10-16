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
        public TileModel()
        {
            Color = Color.Red;
            Title = string.Empty;
            Size = new TileSize(0, 0);
            Position = new Position() { Column = 0, Row = 0 };
        }

        public Color Color { get; set; }

        public string Title { get; set; }

        public TileSize Size { get; set; }

        public Position Position { get; set; }
    }
}
