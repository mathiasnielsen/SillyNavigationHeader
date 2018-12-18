using System;
using UIKit;

namespace TestingTopHeader
{
    public class Page1Translucent : ViewBase
    {
        private UIScrollView _scrollView;

        private UIImageView _imageView;
        private UILabel _titleLabel;

        protected override void OnPrepareUIElements()
        {
            Title = "Playground";
            View.BackgroundColor = UIColor.White;

            NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Trash);
            NavigationItem.RightBarButtonItem.Clicked += (object sender, EventArgs e) =>
            {
                NavigationController.PushViewController(new Page2Tranclucent(), true);
            };

            // BAD SOLUTION 2 (since this could be done from the NavigationController itself).
            // GOOD THING: We have the navigation y off set (equal to statusbarheight)
            ////SetCustomTranslucentNavigationBarStyle();
            InitializeUIElements();

            SetupLayoutConstraints();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            NavigationController.NavigationBar.PrefersLargeTitles = true;
            NavigationController.NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Never;
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            _scrollView.LayoutIfNeeded();
            _scrollView.ContentSize = new CoreGraphics.CGSize(0, View.Frame.Size.Height + 100);
        }

        private void InitializeUIElements()
        {
            _scrollView = new UIScrollView
            {
                AlwaysBounceVertical = true,
            };

            View.AddSubview(_scrollView);

            _imageView = new UIImageView()
            {
                Image = UIImage.FromFile("Assets/test_image.png"),
                ContentMode = UIViewContentMode.ScaleAspectFit,
                BackgroundColor = UIColor.Purple,
                ClipsToBounds = true,
            };

            _scrollView.AddSubview(_imageView);

            _titleLabel = new UILabel
            {
                Text = "Testing the navigationbar translucency",
            };

            _scrollView.AddSubview(_titleLabel);
        }

        private void SetCustomTranslucentNavigationBarStyle()
        {
            if (NavigationController != null && NavigationController.NavigationBar != null)
            {
                var blurEffect = UIBlurEffectStyle.Light;
                var blurView = new UIVisualEffectView(UIBlurEffect.FromStyle(blurEffect));

                var navigationBar = NavigationController.NavigationBar;
                navigationBar.AddSubview(blurView);
                navigationBar.SendSubviewToBack(blurView);
                FullyConstraintTopLayouts(blurView, navigationBar);
            }
        }

        private void FullyConstraintTopLayouts(UIView subview, UINavigationBar navigationBar)
        {
            subview.TranslatesAutoresizingMaskIntoConstraints = false;
            subview.LeftAnchor.ConstraintEqualTo(navigationBar.LeftAnchor).Active = true;
            subview.RightAnchor.ConstraintEqualTo(navigationBar.RightAnchor).Active = true;
            subview.TopAnchor.ConstraintEqualTo(navigationBar.TopAnchor, -navigationBar.Frame.Y).Active = true;
            subview.BottomAnchor.ConstraintEqualTo(navigationBar.BottomAnchor).Active = true;
        }

        private void SetupLayoutConstraints()
        {
            // No adjustments, ergo, scrollview starts from the top of the screen.
            ////_scrollView.ContentInsetAdjustmentBehavior = UIScrollViewContentInsetAdjustmentBehavior.Never;

            _scrollView.TranslatesAutoresizingMaskIntoConstraints = false;
            _scrollView.LeftAnchor.ConstraintEqualTo(View.LeftAnchor).Active = true;
            _scrollView.RightAnchor.ConstraintEqualTo(View.RightAnchor).Active = true;
            _scrollView.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            _scrollView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;

            _titleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            _titleLabel.LeftAnchor.ConstraintEqualTo(_scrollView.LeftAnchor).Active = true;
            _titleLabel.RightAnchor.ConstraintEqualTo(_scrollView.RightAnchor).Active = true;
            _titleLabel.TopAnchor.ConstraintEqualTo(_scrollView.TopAnchor).Active = true;

            _imageView.TranslatesAutoresizingMaskIntoConstraints = false;
            _imageView.LeftAnchor.ConstraintEqualTo(View.LeftAnchor).Active = true;
            _imageView.RightAnchor.ConstraintEqualTo(View.RightAnchor).Active = true;
            _imageView.HeightAnchor.ConstraintEqualTo(90).Active = true;
            _imageView.TopAnchor.ConstraintEqualTo(_titleLabel.BottomAnchor, 20).Active = true;
        }
    }
}
