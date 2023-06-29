
using System.Windows.Input;

using WPLauncher.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//TODO: move event behaviours from square tile and wide tile to a base class to prevent code duplication

namespace WPLauncher
{
    public class DropEventArgs
    {
        public double TranslationX { get; set; }
        public double TranslationY { get; set; }
        public TileModel TileModel { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SquareTile : ContentView
    {
        public static readonly BindableProperty OnDropProperty = BindableProperty.Create(nameof(OnDrop), typeof(ICommand), typeof(SquareTile));
        public static readonly BindableProperty OnDragProperty = BindableProperty.Create(nameof(OnDrag), typeof(ICommand), typeof(SquareTile));
        public static readonly BindableProperty TileModelProperty = BindableProperty.Create(nameof(TileModel), typeof(TileModel), typeof(SquareTile));

        public ICommand OnDrop
        {
            get
            {
                return GetValue(OnDropProperty) as ICommand;
            }
            set
            {
                SetValue(OnDropProperty, value);
            }
        }

        public ICommand OnDrag
        {
            get
            {
                return GetValue(OnDragProperty) as ICommand;
            }
            set
            {
                SetValue(OnDragProperty, value);
            }
        }

        public TileModel TileModel
        {
            get
            {
                return GetValue(TileModelProperty) as TileModel;
            }
            set
            {  
                SetValue(TileModelProperty, value);
            }
        }

        public SquareTile()
        {
            InitializeComponent();
            AddPanGestureRecognizer();
        }

        public void AddPanGestureRecognizer()
        {
            var gr = new PanGestureRecognizer();
            gr.PanUpdated += Gr_PanUpdated;
            this.GestureRecognizers.Add(gr);
        }

        private void Gr_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            var tile = sender as SquareTile;
            if (TileModel.PanEnabled)
            {

                if (e.StatusType == GestureStatus.Running)
                {
                    tile.TranslationX += e.TotalX;
                    tile.TranslationY += e.TotalY;

                    OnDrag.Execute(new DropEventArgs
                    {
                        TileModel = TileModel,
                        TranslationX = tile.TranslationX,
                        TranslationY = tile.TranslationY
                    });
                }
                else if (e.StatusType == GestureStatus.Completed)
                {
                    var dropEventArgs = new DropEventArgs()
                    {
                        TranslationX = tile.TranslationX,
                        TranslationY = tile.TranslationY,
                        TileModel = TileModel,
                    };

                    OnDrop.Execute(dropEventArgs);

                    // Reset translation after drop as its only needed during dragging
                    tile.TranslationX = 0;
                    tile.TranslationY = 0;
                }
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, width);
            HeightRequest = width;
        }
    }
}