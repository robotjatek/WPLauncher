using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace WPLauncher.Pages
{
    public class StartPage : MultiPage<ContentPage>
    {
        private readonly TilePage _tilePage;
        private readonly AppListPage _appListPage;

        private readonly PanGestureRecognizer _panGestureRecognizer;
        private int _currentPageID = 0;

        private bool? _movementX = null;
        private double _totalX = 0;
        private double _totalY = 0;

        private double PanoramaWidth => Children.Sum(X => X.Width);

        public StartPage(TilePage tilePage, AppListPage applistPage) : base()
        {
           // NavigationPage.SetHasNavigationBar(this, false); // TODO: this fucks up the contentsize in the pages...
            _tilePage = tilePage;
            _appListPage = applistPage;
            this.Children.Add(tilePage);
            this.Children.Add(applistPage);

            CurrentPage = tilePage;

            _panGestureRecognizer = new PanGestureRecognizer();
            _panGestureRecognizer.PanUpdated += PanGestureRecognizer_PanUpdated;

            _tilePage.Content.GestureRecognizers.Add(_panGestureRecognizer);
            _appListPage.AddGestureRecognizer(_panGestureRecognizer);
            _appListPage.Content.GestureRecognizers.Add(_panGestureRecognizer);
        }

        private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Running)
            {
                _totalX += e.TotalX;
                _totalY += e.TotalY;

                // Determine if the movement is along the X or the Y axis
                if (!_movementX.HasValue && (Math.Abs(_totalX) > 3 || Math.Abs(_totalY) > 3))
                {
                    _movementX = Math.Abs(_totalX) > Math.Abs(_totalY);
                }

                // Only allow translation on one axis at a time, restrict the other axis while the gesture is running
                if (_movementX.HasValue && _movementX.Value)
                {
                    TranslateX(e.TotalX);
                }
                else if (_movementX.HasValue)
                {
                    TranslateY(e.TotalY);
                }
            }
            else if (e.StatusType == GestureStatus.Completed)
            {
                // If the amount of movement is not enough jump back to the current page
                if (Math.Abs(_totalX) < 100 && _movementX.HasValue)
                {
                    JumpToPage(_currentPageID);
                }
                else if (_movementX.HasValue && _movementX.Value)
                {
                    var page = CalculatePageId(_totalX);
                    if (page >= 0 && page < Children.Count)
                        JumpToPage(CalculatePageId(_totalX));
                    else
                        JumpToPage(_currentPageID);
                }

                _totalX = 0;
                _totalY = 0;
                _movementX = null;
            }
        }

        private void TranslateX(double x)
        {
            var windowPosition = _currentPageID * CurrentPage.Width;
            var windowWidth = CurrentPage.Width; // any page will do as all of them are the same width

            var panoramaWidth = PanoramaWidth;
            var totalTranslation = Children.Sum(c => c.TranslationX);
            var t = windowPosition - x;

            if (t > 0 && t + windowWidth < panoramaWidth)
            {
                foreach (var child in Children)
                {
                    child.TranslationX += x;
                }
            }
        }

        private void TranslateY(double y)
        {
            var translation = CurrentPage.Content.TranslationY + y;
            var min = Height - CurrentPage.Content.Height;
            var max = 0;

            CurrentPage.Content.TranslationY = translation.Clamp(min, max);
        }

        private void JumpToPage(int page)
        {
            if (page < 0 || page >= Children.Count)
                throw new ArgumentOutOfRangeException(nameof(page));

            ResetTranslations();
            var totalTranslationX = page * CurrentPage.Width; // assuming that every page has equal width
            foreach (var child in Children)
            {
                child.TranslationX -= totalTranslationX;
            }
            _currentPageID = page;
            CurrentPage = Children[page];
        }

        private int CalculatePageId(double xMovement)
        {
            if (xMovement < 0)
                return _currentPageID + 1;
            else
                return _currentPageID - 1;
        }

        private void ResetTranslations()
        {
            var tx = 0.0;
            foreach (var child in Children)
            {
                child.TranslationX = tx;
                tx += child.Width;
            }
            CurrentPage = Children.First();
            _currentPageID = 0;
            _totalX = 0;
            _totalY = 0;
            _movementX = null;
        }

        protected override ContentPage CreateDefault(object item)
        {
            return new ContentPage();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            ResetTranslations();
        }
    }
}
