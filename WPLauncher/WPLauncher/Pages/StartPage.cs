
using System.Linq;

using Xamarin.Forms;

namespace WPLauncher.Pages
{

    public class CustomCarouselPage : MultiPage<ContentPage>
    {
        private SwipeGestureRecognizer _leftSwipeRecognizer;
        private int _currentPageID = 0;

        public CustomCarouselPage()
        {
            _leftSwipeRecognizer = new SwipeGestureRecognizer
            {
                Direction = SwipeDirection.Left
            };
            _leftSwipeRecognizer.Swiped += OnLeftSwipe;
        }

        private void OnLeftSwipe(object sender, SwipedEventArgs s)
        {
            _currentPageID++;
            if (_currentPageID > Children.Count - 1)
            {
                _currentPageID = 0;
            }

            CurrentPage = Children[_currentPageID];
            CurrentPage.TranslationX = -CurrentPage.Width;
        }

        protected override ContentPage CreateDefault(object item)
        {
            return new ContentPage();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // TODO: implement
        }

        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);
            if (child is ContentPage page)
            {
                page.Content.GestureRecognizers.Add(_leftSwipeRecognizer);
            }

            CurrentPage = Children.FirstOrDefault();
        }

        protected override void OnChildRemoved(Element child, int oldLogicalIndex)
        {
            base.OnChildRemoved(child, oldLogicalIndex);
            if (child is ContentPage page)
            {
                page.Content.GestureRecognizers.Remove(_leftSwipeRecognizer);
                page.TranslationX = 0;
            }
        }
    }

    public class StartPage : CarouselPage
    {
        private readonly TilePage _tilePage;
        private readonly AppListPage _appListPage;

        public StartPage(TilePage tilePage, AppListPage applistPage)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            _tilePage = tilePage;
            _appListPage = applistPage;
            this.Children.Add(applistPage);
            this.Children.Add(tilePage);
        }
    }
}
