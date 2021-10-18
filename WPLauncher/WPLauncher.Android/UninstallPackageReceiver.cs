using Android.Content;

using WPLauncher.Services;

namespace WPLauncher.Droid
{
    public class UninstallPackageReceiver : BroadcastReceiver
    {
        private readonly ITileService _tileService;

        public UninstallPackageReceiver(ITileService tileService)
        {
            _tileService = tileService;
        }

        public override void OnReceive(Context context, Intent intent)
        {
            var packageUri = intent.DataString;
            var package = packageUri.Substring("package:".Length);
            _tileService.UnpinTile(package);
        }

        public void Register(Context context)
        {
            var filter = new IntentFilter();
            filter.AddAction(Intent.ActionPackageFullyRemoved);
            filter.AddDataScheme("package");
            context.RegisterReceiver(this, filter);
        }

        public void Unregister(Context context)
        {
            context.UnregisterReceiver(this);
        }
    }
}