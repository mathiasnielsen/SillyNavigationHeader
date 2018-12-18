using System;
using System.Linq;
using UIKit;

namespace TestingTopHeader
{
    public class Page2Tranclucent : ViewBase
    {
        private UIScrollView _scrollView;
        private UILabel _titleLabel;
        private UIImageView _imageView;

        protected override void OnPrepareUIElements()
        {
            Title = "Another playground";
            View.BackgroundColor = UIColor.Orange;
            NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Done);
            InitializeUIElements();
            SetupLayoutConstraints();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.NavigationBar.PrefersLargeTitles = false;
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
            _scrollView = new UIScrollView()
            {
                AlwaysBounceVertical = true,
            };

            View.AddSubview(_scrollView);

            _titleLabel = new UILabel()
            {
                Text = "Another page",
            };

            _scrollView.AddSubview(_titleLabel);

            _imageView = new UIImageView()
            {
                Image = UIImage.FromFile("Assets/test_image.png"),
                ContentMode = UIViewContentMode.ScaleAspectFit,
                BackgroundColor = UIColor.Purple,
                ClipsToBounds = true,
            };

            _scrollView.AddSubview(_imageView);

        }

        private void SetupLayoutConstraints()
        {
            AutoLayoutToolBox.AlignToFullConstraints(_scrollView, View);

            AutoLayoutToolBox.AlignTopAnchorTopOf(_titleLabel, _scrollView);
            AutoLayoutToolBox.AlignLeftAnchorToLeftOf(_titleLabel, _scrollView);
            AutoLayoutToolBox.AlignRightAnchorToRightOf(_titleLabel, _scrollView);

            AutoLayoutToolBox.AlignTopToBottomOf(_imageView, _titleLabel);
            AutoLayoutToolBox.AlignLeftAnchorToLeftOf(_imageView, View);
            AutoLayoutToolBox.AlignRightAnchorToRightOf(_imageView, View);
        }
    }
}
