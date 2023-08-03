using System.Linq;

using WPLauncher.Models;
using WPLauncher.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WPLauncher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TilePage : ContentPage
    {
        private readonly TilePageViewModel vm;
        private readonly BoxView _dropTarget;
        private double _cellWidth;

        public TilePage(TilePageViewModel vm)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.vm = vm;
            this.BindingContext = vm;
            vm.PropertyChanged += Vm_PropertyChanged;

            // TODO: replace this with a preview of the tile
            _dropTarget = new BoxView()
            {
                BackgroundColor = AccentColors.Cobalt,
                Opacity = 0.7,
            };
        }

        private async void SwipeGesture_Swiped(object sender, SwipedEventArgs e)
        {
            if(e.Direction == SwipeDirection.Left)
            {
                await Shell.Current.GoToAsync(nameof(AppListPage), true);
            }
        }

        public void ShowDropTarget(int column, int row, TileModel tileModel)
        {
            // Add droptarget to the grid
            this.tileGrid.Children.Add(
            _dropTarget,
            tileModel.Position.Column,
            tileModel.Position.Column + tileModel.Size.Width, // col span
            tileModel.Position.Row,
            tileModel.Position.Row + tileModel.Size.Height); // row span

            // Animate droptarget to the new position. TranslateTo is relative to the current gridPosition
            _dropTarget.TranslateTo(
                (column - tileModel.Position.Column) * _cellWidth,
                (row - tileModel.Position.Row) * _cellWidth,
                100);
        }

        public void HideDropTarget()
        {
            _dropTarget.TranslateTo(0, 0);
            this.tileGrid.Children.Remove(_dropTarget);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.TilePageRef = this;
            RecalculateGridDimensions();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            // Onappearing has wrong sizedata at the first run so grid dimensions have to be recalculated when OnSizeAllocated fires
            base.OnSizeAllocated(width, height);
            vm.TilePageRef = this;
            _cellWidth = this.Width / 4; // TODO: Columnsize can be configured in the future
            RecalculateGridDimensions();
        }

        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.vm.TileModels))
            {
                RecalculateGridDimensions();
            }
        }

        private void RecalculateGridDimensions()
        {
            if (vm.TileModels.Any())
            {
                var heightWithLowestTile = vm.TileModels.DefaultIfEmpty(new TileModel()).Max(t => t.Position.Row + t.Size.Height) * _cellWidth;

                //this.scroller.ForceLayout();
                this.tileGrid.Layout(new Rectangle(0, 0, this.tileGrid.Width, heightWithLowestTile));
            }
        }
    }
}