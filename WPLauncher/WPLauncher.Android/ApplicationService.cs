using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Net;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using WPLauncher.Services;

using Xamarin.Forms;

namespace WPLauncher.Droid
{
    public class ApplicationService : IApplicationService
    {
        private readonly ConcurrentDictionary<string, AppProperties> _applicationCache = new ConcurrentDictionary<string, AppProperties>();

        public bool IsInstalled(AppProperties app)
        {
            try
            {
                Android.App.Application.Context.PackageManager.GetPackageInfo(app.PackageName, PackageInfoFlags.Activities);
                return true;
            }
            catch (PackageManager.NameNotFoundException)
            {
                return false;
            }
        }

        public void UninstallApplication(AppProperties app)
        {
            var deleteIntent = new Intent(Intent.ActionDelete);
            deleteIntent.AddFlags(ActivityFlags.NewTask);
            deleteIntent.SetData(Uri.Parse($"package:{app.PackageName}"));
            deleteIntent.PutExtra(Intent.ExtraReturnResult, true);
            Android.App.Application.Context.StartActivity(deleteIntent);

            _applicationCache.TryRemove(app.PackageName, out _);
        }

        public async Task<IEnumerable<AppProperties>> GetApplicationList()
        {
            var packageManager = Android.App.Application.Context.PackageManager;

            var mainIntent = new Intent(Intent.ActionMain, null);
            mainIntent.AddCategory(Intent.CategoryLauncher);
            var apps = packageManager.QueryIntentActivities(mainIntent, 0);

            var appProperties = apps
                .AsParallel()
                .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                .Select(async (app) =>
                {
                    var packageName = app.ActivityInfo.PackageName;

                    if (!_applicationCache.ContainsKey(packageName))
                    {
                        var name = app.LoadLabel(packageManager);
                        var icon = app.LoadIcon(packageManager);
                        var intent = packageManager.GetLaunchIntentForPackage(packageName);

                        var properties = new AppProperties(name, packageName, await ToImageSource(icon), new AndroidRunnable(intent));
                        _applicationCache.TryAdd(packageName, properties);
                        return properties;
                    }

                    _applicationCache.TryGetValue(packageName, out AppProperties cachedValue);
                    return cachedValue;
                });

            return await Task.WhenAll(appProperties);
        }

        //TODO: Optimize this. I think its also leaking memory
        private async Task<ImageSource> ToImageSource(Drawable drawable)
        {
            var ms = new MemoryStream();
            await ToBitmap(drawable).CompressAsync(Bitmap.CompressFormat.Png, 100, ms);
            return ImageSource.FromStream(() =>
            {
                var ret = new MemoryStream(ms.ToArray());
                return ret;
            });
        }

        private Bitmap ToBitmap(Drawable drawable)
        {
            var bitmap = Bitmap.CreateBitmap(drawable.IntrinsicWidth, drawable.IntrinsicHeight, Bitmap.Config.Argb8888);
            var canvas = new Canvas(bitmap);
            drawable.SetBounds(0, 0, canvas.Width, canvas.Height);
            drawable.Draw(canvas);

            return bitmap;
        }

        private Bitmap EmptyBitmap()
        {
            var bitmap = Bitmap.CreateBitmap(32, 32, Bitmap.Config.Argb8888);
            var canvas = new Canvas(bitmap);
            canvas.DrawRGB(255, 255, 0);

            return bitmap;
        }
    }
}