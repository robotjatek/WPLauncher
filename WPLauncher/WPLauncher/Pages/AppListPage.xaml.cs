
using System.Collections.Generic;

using WPLauncher.ViewModels;

using Xamarin.Forms;

namespace WPLauncher
{
    public partial class AppListPage : ContentPage
    {
        private readonly AppListViewModel appListViewModel;
        private readonly List<GestureRecognizer> gestureRecognizers = new List<GestureRecognizer>();

        public AppListPage(AppListViewModel appListViewModel)
        {
            InitializeComponent();
            this.appListViewModel = appListViewModel;
            BindingContext = appListViewModel;

            this.listViewThing.ChildAdded += ListViewThing_ChildAdded;
        }

        private void ListViewThing_ChildAdded(object sender, ElementEventArgs e)
        {
            if (e.Element is StackLayout layout)
            {
                layout.GestureRecognizers.Add(gestureRecognizers[0]);
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await appListViewModel.InitCollection();
        }

        public void AddGestureRecognizer(GestureRecognizer recognizer)
        {
            gestureRecognizers.Add(recognizer);
        }
    }
}
