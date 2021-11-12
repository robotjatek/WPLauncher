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
        private readonly ILauncherApplicationService _launcherApplicationService;
        private readonly string _iconCachePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "IconCache");

        public ApplicationService(ILauncherApplicationService launcherApplicationService)
        {
            _launcherApplicationService = launcherApplicationService;
            
            if(!Directory.Exists(_iconCachePath))
            {
                Directory.CreateDirectory(_iconCachePath);
            }
        }

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

        public void ClearCache()
        {
            _applicationCache.Clear();
            Directory.Delete(_iconCachePath, true);
            Directory.CreateDirectory(_iconCachePath);
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
                        using var icon = app.LoadIcon(packageManager); //TODO: load adaptive icon
                        var intent = packageManager.GetLaunchIntentForPackage(packageName);
                        var properties = new AppProperties(name, packageName, await ToImageSource(icon, packageName), new AndroidRunnable(intent));
                        _applicationCache.TryAdd(packageName, properties);

                        return properties;
                    }

                    _applicationCache.TryGetValue(packageName, out AppProperties cachedValue);
                    return cachedValue;
                });

            var installedApps = await Task.WhenAll(appProperties);
            return installedApps.Concat(await _launcherApplicationService.GetLauncherApplications());
        }

        private async Task<ImageSource> ToImageSource(Drawable drawable, string cacheKey)
        {
            var filePath = System.IO.Path.Combine(_iconCachePath, cacheKey);
            await WriteToCache(drawable, filePath);

            return ImageSource.FromFile(filePath);
        }

        private async Task WriteToCache(Drawable drawable, string filePath)
        {
            if (!File.Exists(filePath) || File.GetCreationTime(filePath) < System.DateTime.Now.AddDays(-7))
            {
                using var stream = new MemoryStream();
                await ToBitmap(drawable).CompressAsync(Bitmap.CompressFormat.Png, 100, stream);
                await File.WriteAllBytesAsync(filePath, stream.ToArray());
            }
        }

        private Bitmap ToBitmap(Drawable drawable)
        {
            var bitmap = Bitmap.CreateBitmap(drawable.IntrinsicWidth, drawable.IntrinsicHeight, Bitmap.Config.Argb8888);
            var canvas = new Canvas(bitmap);
            drawable.SetBounds(0, 0, canvas.Width, canvas.Height);
            drawable.Draw(canvas);

            return bitmap;
        }
    }
}