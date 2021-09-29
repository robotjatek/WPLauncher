using System.Collections.Generic;

using WPLauncher.Components;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using static WPLauncher.TileSizeDefinitions;

namespace WPLauncher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TilePage : ContentPage
    {
        private readonly TileSizeDefinitions tileSizeDefinitions = new TileSizeDefinitions();

        public TilePage()
        {
            InitializeComponent();

            var tiles = new View[]
            {
                 CreateTile(Color.Green, 0, 0, TileSizeMode.Medium),
                 CreateTile(Color.Red, 0, 2, TileSizeMode.Medium),
                 CreateTile(Color.Red, 2, 0, TileSizeMode.Medium),
                 CreateTile(Color.Green, 2, 2, TileSizeMode.Medium),
                 CreateTile(Color.Red, 4, 0, TileSizeMode.Medium),
                 CreateTile(Color.Green, 4, 2, TileSizeMode.Medium),
                 CreateTile(Color.Red, 6, 0, TileSizeMode.Medium),
                 CreateTile(Color.Green, 6, 2, TileSizeMode.Medium),

                 CreateTile(Color.Blue, 8, 0, TileSizeMode.Small),
                 CreateTile(Color.Blue, 8, 1, TileSizeMode.Small),
                 CreateTile(Color.Blue, 8, 2, TileSizeMode.Small),
                 CreateTile(Color.Blue, 8, 3, TileSizeMode.Small),

                 CreateTile(Color.YellowGreen, 9, 0, TileSizeMode.Large),

                 CreateTile(Color.Coral, 13, 0, TileSizeMode.Wide),

                 CreateTile(Color.FromHex("1BA1E2"), 15, 1, TileSizeMode.Medium),

                 CreateTile(Color.Blue, 15, 0, TileSizeMode.Small),
                 CreateTile(Color.Blue, 16, 0, TileSizeMode.Small),
                 CreateTile(Color.Blue, 15, 3, TileSizeMode.Small),
                 CreateTile(Color.Blue, 16, 3, TileSizeMode.Small),
            };

            AddChildren(tiles);
        }

        private void AddChildren(IEnumerable<View> children)
        {
            foreach (var child in children)
            {
                MainGrid.Children.Add(child);
            }
        }

        private View CreateTile(Color color, int row, int column, TileSizeMode size)
        {
            if (size == TileSizeMode.Wide)
            {
                return CreateWideTile(color, row, column, tileSizeDefinitions.GetTileSize(size));
            }
            else
            {
                return CreateSquareTile(color, row, column, tileSizeDefinitions.GetTileSize(size));
            }
        }

        private WideTile CreateWideTile(Color color, int row, int column, TileProperties size)
        {
            var tile = new WideTile
            {
                BackgroundColor = color,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Content = new StaticIcon
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                }
            };

            SetGridMode(row, column, size, tile);

            return tile;
        }

        private SquareTile CreateSquareTile(Color color, int row, int column, TileProperties size)
        {
            var tile = new SquareTile
            {
                BackgroundColor = color,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Content = new StaticIcon
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                }
            };

            SetGridMode(row, column, size, tile);

            return tile;
        }

        private void SetGridMode(int row, int column, TileProperties size, View tile)
        {
            Grid.SetColumnSpan(tile, size.Width);
            Grid.SetRowSpan(tile, size.Height);
            Grid.SetRow(tile, row);
            Grid.SetColumn(tile, column);
        }

    }
}