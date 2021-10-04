
using Autofac;

using WPLauncher.ViewModels;

namespace WPLauncher.Droid
{
    public class IocConfig
    {
        public IContainer Container { get; private set; }

        public IocConfig()
        {
            var builder = new ContainerBuilder();
            builder.Register(c => new ApplicationService()).As<IApplicationService>().SingleInstance();
            builder.RegisterType<AppListViewModel>().SingleInstance();
            builder.RegisterType<App>().AsSelf().SingleInstance();

            Container = builder.Build();
        }
    }
}