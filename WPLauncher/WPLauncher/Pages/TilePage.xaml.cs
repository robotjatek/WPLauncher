
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

        public TilePage(TilePageViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            this.BindingContext = vm;
            vm.PropertyChanged += Vm_PropertyChanged;

            _dropTarget = new BoxView()
            {
                BackgroundColor = Color.Red,
            };
        }

        public void ShowDropTarget(int column, int row, TileModel tileModel)
        {
            this.tileGrid.Children.Add(
                _dropTarget,
                column,
                column + tileModel.Size.Width, //col span
                row,
                row + tileModel.Size.Height); //row span
        }

        public void HideDropTarget()
        {
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
                var cellWidth = this.Width / 4; //TODO: Columnsize can be configured in the future
                var heightWithLowestTile = vm.TileModels.DefaultIfEmpty(new TileModel()).Max(t => t.Position.Row + t.Size.Height) * cellWidth;

                //this.scroller.ForceLayout();
                this.tileGrid.Layout(new Rectangle(0, 0, this.tileGrid.Width, heightWithLowestTile));
            }
        }
    }
}