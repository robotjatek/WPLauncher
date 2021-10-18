
using Xamarin.Forms;

namespace WPLauncher
{
    public class AppProperties
    {
        public string ReadableName { get; private set; }
        public string PackageName { get; private set; }
        public ImageSource Icon { get; private set; }
        public IRunnable Runnable { get; private set; }

        public AppProperties(string name, string packageName, ImageSource icon, IRunnable runnable)
        {
            ReadableName = name;
            Icon = icon;
            Runnable = runnable;
            PackageName = packageName;
        }

        public void RunApplication()
        {
            Runnable.Run();
        }
    }
}
