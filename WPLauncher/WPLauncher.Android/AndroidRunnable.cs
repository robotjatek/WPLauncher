using Android.Content;

namespace WPLauncher.Droid
{
    public class AndroidRunnable : IRunnable
    {
        private readonly Intent launchIntent;

        public AndroidRunnable(Intent launchIntent)
        {
            this.launchIntent = launchIntent;
        }

        public void Run()
        {
            if (launchIntent != null)
            {
                Android.App.Application.Context.StartActivity(launchIntent);
            }
        }
    }
}