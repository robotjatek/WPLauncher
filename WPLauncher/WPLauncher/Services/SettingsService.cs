using Xamarin.Forms;

namespace WPLauncher.Services
{
    public static class AccentColors
    {
        public static Color Lime { get; } = Color.FromRgb(164, 196, 0);
        public static Color Green { get; } = Color.FromRgb(96, 169, 23);
        public static Color Emerald { get; } = Color.FromRgb(0, 138, 0);
        public static Color Teal { get; } = Color.FromRgb(0, 117, 169);
        public static Color Cyan { get; } = Color.FromRgb(27, 161, 226);
        public static Color Cobalt { get; } = Color.FromRgb(0, 80, 239);
        public static Color Indigo { get; } = Color.FromRgb(106, 0, 255);
        public static Color Violet { get; } = Color.FromRgb(170, 0, 255);
        public static Color Pink { get; } = Color.FromRgb(244, 114, 208);
        public static Color Magenta { get; } = Color.FromRgb(216, 0, 115);
        public static Color Crimson { get; } = Color.FromRgb(162, 0, 37);
        public static Color Red { get; } = Color.FromRgb(229, 20, 0);
        public static Color Orange { get; } = Color.FromRgb(250, 104, 0);
        public static Color Amber { get; } = Color.FromRgb(240, 163, 10);
        public static Color Yellow { get; } = Color.FromRgb(227, 200, 0);
        public static Color Brown { get; } = Color.FromRgb(130, 90, 44);
        public static Color Olive { get; } = Color.FromRgb(109, 135, 100);
        public static Color Steel { get; } = Color.FromRgb(100, 118, 135);
        public static Color Mauve { get; } = Color.FromRgb(118, 96, 138);
        public static Color Taupe { get; } = Color.FromRgb(135, 121, 78);
    }

    public class SettingsService : ISettingsService
    {
        public Color AccentColor { get; set; } = AccentColors.Cobalt;
    }
}
