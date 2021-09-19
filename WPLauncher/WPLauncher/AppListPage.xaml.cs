
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;

namespace WPLauncher
{
    public partial class AppListPage : ContentPage
    {
        public ObservableCollection<AppProperties> AppList { get; private set; }
        private readonly IApplicationService applicationService;
        private bool loaded = false; //TODO: what to do when the applist is updated?

        public AppListPage(IApplicationService applicationService)
        {
            InitializeComponent();
            this.applicationService = applicationService;
            this.Appearing += OnAppearing;
            this.AppList = new ObservableCollection<AppProperties>();
            this.listViewThing.ItemsSource = this.AppList;
            this.listViewThing.ItemTapped += OnItemTapped;
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var app = e.Item as AppProperties;
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
