using System;
using System.Linq;
using UIKit;

namespace TestingTopHeader
{
    public class AnotherTranslucentViewController : ViewBase
    {
        private UIScrollView _scrollView;
        private UILabel _titleLabel;
        private UIImageView _imageView;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Another playground";
            View.BackgroundColor = UIColor.Orange;
            NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Done);

            PrepareUIElements();
            SetupLayoutConstraints();
        }

        private void PrepareUIElements()
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
