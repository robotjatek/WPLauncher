
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

        public TilePage(TilePageViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            this.BindingContext = vm;
            vm.PropertyChanged += Vm_PropertyChanged;
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
            var cellWidth = this.tileGrid.Width / 4; //TODO: Columnsize can be configured in the future
            var gridHeight = vm.TileModels.DefaultIfEmpty(new TileModel()).Max(t => t.Position.Row + t.Size.Height) * cellWidth;

            this.scroller.ForceLayout();
            this.tileGrid.Layout(new Rectangle(0, 0, this.tileGrid.Width, gridHeight));
        }
    }
}