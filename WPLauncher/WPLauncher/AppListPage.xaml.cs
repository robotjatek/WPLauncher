
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using Xamarin.Forms;

namespace WPLauncher
{
    public partial class AppListPage : ContentPage
    {
        public ObservableCollection<AppProperties> AppList { get; } = new ObservableCollection<AppProperties>();

        public ICommand HandleItemTapped => new Command((selected) =>
        {
            OnItemTapped(selected as AppProperties);
        });

        public ICommand LongPressCommand => new Command((selected) =>
        {
            OnItemTapped(selected as AppProperties);
        });

        private readonly IApplicationService applicationService;
        private bool loaded = false; //TODO: what to do when the applist is updated?

        public AppListPage(IApplicationService applicationService)
        {
            InitializeComponent();
            this.BindingContext = this;
            this.applicationService = applicationService;
            this.Appearing += OnAppearing;
        }

        private void OnItemTapped(AppProperties app)
        {
            app.RunApplication();
        }

        private async void OnAppearing(object sender, System.EventArgs e)
        {
            if (!loaded)
            {
                var applistOrdered = (await applicationService.GetApplicationList()).OrderBy(a => a.Name);
                foreach (var app in applistOrdered)
                {
                    this.AppList.Add(app);
                }
                loaded = true;
            }
        }
    }
}
