﻿
using Autofac;

using WPLauncher.Pages;
using WPLauncher.Services;
using WPLauncher.ViewModels;

namespace WPLauncher.Droid
{
    public class IocConfig
    {
        public IContainer Container { get; private set; }

        public IocConfig()
        {
            var builder = new ContainerBuilder();

            RegisterServices(builder);
            RegisterViewModels(builder);
            RegisterPages(builder);

            builder.RegisterType<App>().AsSelf().SingleInstance();

            Container = builder.Build();
        }

        private void RegisterPages(ContainerBuilder builder)
        {
            builder.RegisterType<TilePage>().SingleInstance();
            builder.RegisterType<AppListPage>().SingleInstance();

            builder.RegisterType<StartPage>().SingleInstance();

            builder.RegisterType<SettingsPage>().SingleInstance();
            builder.RegisterType<AccentColorListPage>().SingleInstance();
        }

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<AppListViewModel>().SingleInstance();
            builder.RegisterType<TilePageViewModel>().SingleInstance();
            builder.RegisterType<SettingsPageViewModel>().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies).SingleInstance();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationService>().As<IApplicationService>().SingleInstance();
            builder.RegisterType<TileService>().As<ITileService>().SingleInstance();
            builder.RegisterType<UninstallPackageReceiver>().SingleInstance();
            builder.RegisterType<SettingsService>().As<ISettingsService>().SingleInstance();
            builder.RegisterType<LauncherApplicationService>().As<ILauncherApplicationService>().SingleInstance();
        }
    }
}