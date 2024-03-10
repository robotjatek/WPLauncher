using WPLauncher.ViewModels;

using static WPLauncher.TileSizeDefinitions;

namespace WPLauncher.Models
{
    public class Position
    {
        public int Column { get; set; }

        public int Row { get; set; }
    }

    //TODO: OnPropertyChanged needs to be in an other baseclass and tilemodel shouldnt implement BaseViewModel
    public class TileModel : BaseViewModel
    {
        private double _scale; // TODO: maybe use xct.toucheffect.pressedscale instead of the custom implementation
        private Position _position;

        public TileModel()
        {
            Title = string.Empty;
            Size = new TileSize(0, 0);
            Position = new Position() { Column = 0, Row = 0 };
            Scale = 1;
        }

        public string Title { get; set; }

        public TileSize Size { get; set; }

        public bool PanEnabled { get; set; } = false;

        public Position Position 
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged();
            }
        }

        public AppProperties AppProperties { get; set; }

        public double Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                OnPropertyChanged();
            }
        }
    }
}
