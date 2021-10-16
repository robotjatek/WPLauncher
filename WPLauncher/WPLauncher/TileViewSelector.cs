using WPLauncher.Models;

using Xamarin.Forms;

namespace WPLauncher
{
    public class TileViewSelector : DataTemplateSelector
    {
        public DataTemplate SquareTileTemplate { get; set; }

        public DataTemplate WideTileTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var model = item as TileModel;
            if (model.Size.Width > model.Size.Height)
            {
                return WideTileTemplate;
            }

            return SquareTileTemplate;
        }
    }
}
