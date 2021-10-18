using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;

using Autofac;

namespace WPLauncher.Droid
{
    [Activity(Label = "WPLauncher", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    [IntentFilter(new[] { Intent.ActionMain }, Categories = new[] { Intent.CategoryHome, Intent.CategoryDefault })]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private UninstallPackageReceiver _uninstallReceiver;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            var ioc = new IocConfig().Container;
            using var scope = ioc.BeginLifetimeScope();

            _uninstallReceiver = ConfigureUninstallReceiver(ioc);

            var app = scope.Resolve<App>();
            LoadApplication(app);
        }

        private UninstallPackageReceiver ConfigureUninstallReceiver(IContainer ioc)
        {
            //TODO: I dont like this locator pattern here. I should look for a better solution. But for the moment it stays like this, Because in the current state the "App" class is on the top of the dependecy hierarchy
            var uninstallReceiver = ioc.Resolve<UninstallPackageReceiver>();
            uninstallReceiver.Register(this);
            return uninstallReceiver;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _uninstallReceiver.Unregister(this);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}