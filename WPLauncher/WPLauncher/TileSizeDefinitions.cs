using System.Collections.Generic;

namespace WPLauncher
{
    public class TileSizeDefinitions
    {
        public TileProperties GetTileSize(TileSizeMode size)
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

        private readonly Dictionary<TileSizeMode, TileProperties> tileSizes = new Dictionary<TileSizeMode, TileProperties>()
        {
            {
                TileSizeMode.Wide,
                new TileProperties(4,2)
            },

            {
                TileSizeMode.Small,
                new TileProperties(1,1)
            },

            {
                TileSizeMode.Medium,
                new TileProperties(2,2)
            },

            {
                TileSizeMode.Large,
                new TileProperties(4,4)
            }
        };

        public class TileProperties
        {
            public int Width { get; private set; }

            public int Height { get; private set; }

            public TileProperties(int width, int height)
            {
                Width = width;
                Height = height;
            }
        }
    }
}
