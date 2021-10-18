
using Autofac;

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
            //TODO: register views

            builder.RegisterType<App>().AsSelf().SingleInstance();

            Container = builder.Build();
        }

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<AppListViewModel>().SingleInstance();
            builder.RegisterType<TilePageViewModel>().SingleInstance();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.Register(c => new ApplicationService()).As<IApplicationService>().SingleInstance();
            builder.RegisterType<TileService>().As<ITileService>().SingleInstance();
            builder.RegisterType<UninstallPackageReceiver>().SingleInstance();
        }
    }
}