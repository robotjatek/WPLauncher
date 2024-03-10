using System.Collections.Generic;

namespace WPLauncher
{
    public class TileSizeDefinitions
    {
        public TileSize GetTileSize(TileSizeMode size)
        {
            return tileSizes[size];
        }

        public enum TileSizeMode
        {
            Small = 0, // 1x1
            Medium = 1, // 2x2
            Large = 2, // 4x4
            Wide = 3 // 4x2
        }

        private readonly Dictionary<TileSizeMode, TileSize> tileSizes = new Dictionary<TileSizeMode, TileSize>()
        {
            {
                TileSizeMode.Wide,
                new TileSize(4,2)
            },

            {
                TileSizeMode.Small,
                new TileSize(1,1)
            },

            {
                TileSizeMode.Medium,
                new TileSize(2,2)
            },

            {
                TileSizeMode.Large,
                new TileSize(4,4)
            }
        };

        public class TileSize
        {
            public int Width { get; private set; }

            public int Height { get; private set; }

            public TileSize(int width, int height)
            {
                Width = width;
                Height = height;
            }

            public int AspectRatio => Width / Height;
        }
    }
}
