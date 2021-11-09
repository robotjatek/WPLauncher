using Xamarin.Forms;

namespace WPLauncher.Models
{
    public class NamedColor
    {
        public string Name { get; set; }

        public Color Color { get; set; }

        public readonly static NamedColor[] NamedColors = new[]
        {
            new NamedColor
            {
                Color = AccentColors.Amber,
                Name = "amber"
            },
            new NamedColor
            {
                Color = AccentColors.Brown,
                Name = "brown"
            },
            new NamedColor
            {
                Color = AccentColors.Cobalt,
                Name = "cobalt"
            },
            new NamedColor
            {
                Color = AccentColors.Crimson,
                Name = "crimson"
            },
            new NamedColor
            {
                Color = AccentColors.Cyan,
                Name = "cyan"
            },
            new NamedColor
            {
                Color = AccentColors.Emerald,
                Name = "emerald"
            },
            new NamedColor
            {
                Color = AccentColors.Green,
                Name = "green"
            },
            new NamedColor
            {
                Color = AccentColors.Indigo,
                Name = "indigo"
            },
            new NamedColor
            {
                Color = AccentColors.Lime,
                Name = "lime"
            },
            new NamedColor
            {
                Color = AccentColors.Magenta,
                Name = "magenta"
            },
            new NamedColor
            {
                Color = AccentColors.Mauve,
                Name = "mauve"
            },
            new NamedColor
            {
                Color = AccentColors.Olive,
                Name = "olive"
            },
            new NamedColor
            {
                Color = AccentColors.Orange,
                Name = "orange"
            },
            new NamedColor
            {
                Color = AccentColors.Pink,
                Name = "pink"
            },
            new NamedColor
            {
                Color = AccentColors.Red,
                Name = "red"
            },
            new NamedColor
            {
                Color = AccentColors.Steel,
                Name = "steel"
            },
            new NamedColor
            {
                Color = AccentColors.Taupe,
                Name = "taupe"
            },
            new NamedColor
            {
                Color = AccentColors.Teal,
                Name = "teal"
            },
            new NamedColor
            {
                Color = AccentColors.Violet,
                Name = "violet"
            },
            new NamedColor
            {
                Color = AccentColors.Yellow,
                Name = "yellow"
            },
        };
    }
}
