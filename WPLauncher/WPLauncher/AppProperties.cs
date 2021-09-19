
using Xamarin.Forms;

namespace WPLauncher
{
    public class AppProperties
    {
        public string Name { get; private set; }
        public ImageSource Icon { get; private set; }
        public IRunnable Runnable { get; private set; }

        public AppProperties(string name, ImageSource icon, IRunnable runnable)
        {
            Name = name;
            Icon = icon;
            Runnable = runnable;
        }

        public void RunApplication()
        {
            Runnable.Run();
        }
    }
}
