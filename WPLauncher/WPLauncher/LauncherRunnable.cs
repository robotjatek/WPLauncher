using WPLauncher.Pages;

using Xamarin.Forms;

namespace WPLauncher
{
    public class LauncherRunnable : IRunnable
    {
        private readonly Page _page;

        public LauncherRunnable(Page page)
        {
            _page = page;
        }

        public void Run()
        {
            Device.BeginInvokeOnMainThread(() => Application.Current.MainPage.Navigation.PushAsync(_page)); //TODO: investigate how to call with "await"
            //await Application.Current.MainPage.Navigation.PushAsync(_page);
        }
    }
}
