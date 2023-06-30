
using System.Windows.Input;

using WPLauncher.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WPLauncher
{
    public class DropEventArgs
    {
        public double TranslationX { get; set; }
        public double TranslationY { get; set; }
        public TileModel TileModel { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TileComponent : ContentView
    {
        public static readonly BindableProperty OnDropProperty = BindableProperty.Create(nameof(OnDrop), typeof(ICommand), typeof(TileComponent));
        public static readonly BindableProperty OnDragProperty = BindableProperty.Create(nameof(OnDrag), typeof(ICommand), typeof(TileComponent));
        public static readonly BindableProperty TileModelProperty = BindableProperty.Create(nameof(TileModel), typeof(TileModel), typeof(TileComponent));

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

        public TileComponent()
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
            var tile = sender as TileComponent;
            if (TileModel.PanEnabled)
            {

                if (e.StatusType == GestureStatus.Running)
                {
                    tile.TranslationX += e.TotalX;
                    tile.TranslationY += e.TotalY;

                    OnDrag.Execute(new DropEventArgs
                    {
                        TranslationX = tile.TranslationX,
                        TranslationY = tile.TranslationY,
                        TileModel = TileModel
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
            var aspectRatio = TileModel != null ? TileModel.Size.AspectRatio : 1;
            base.OnSizeAllocated(width, width / aspectRatio);
            HeightRequest = width / aspectRatio;
        }
    }
}