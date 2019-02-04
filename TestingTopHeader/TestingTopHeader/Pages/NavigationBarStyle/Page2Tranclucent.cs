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
        private UIButton _testButton1;
        private UIButton _testButton2;

        protected override void OnPrepareUIElements()
        {
            Title = "Another playground";
            View.BackgroundColor = UIColor.Orange;
            NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Done);
            InitializeUIElements();
            SetupLayoutConstraints();

            NavigationController.NavigationBar.PrefersLargeTitles = true;
            NavigationController.NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Always;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
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

            InitializeTestButton1();
            InitializeTestButton2();
        }

        private void InitializeTestButton1()
        {
            _testButton1 = new UIButton(UIButtonType.System);
            _testButton1.SetTitle("Hide/Show navigationbar", UIControlState.Normal);
            _testButton1.TouchUpInside += (sender, e) =>
            {
                if (NavigationController.NavigationBar.Hidden)
                {
                    NavigationController.SetNavigationBarHidden(false, true);
                }
                else
                {
                    NavigationController.SetNavigationBarHidden(true, true);
                }
            };

            _scrollView.AddSubview(_testButton1);
        }

        private void InitializeTestButton2()
        {
            _testButton2 = new UIButton(UIButtonType.System);
            _testButton2.SetTitle("Hide/Show statusbar", UIControlState.Normal);
            _testButton2.TouchUpInside += (sender, e) =>
            {
                // In order for the these methods to work, apply "View controller-based status bar appearance" to false in the info.plist.
                // If you do not want to do this, then you can override the "PrefersStatusBarHidden" property.
                if (UIApplication.SharedApplication.StatusBarHidden)
                {
                    UIApplication.SharedApplication.SetStatusBarHidden(false, UIStatusBarAnimation.Slide);
                }
                else
                {
                    UIApplication.SharedApplication.SetStatusBarHidden(true, UIStatusBarAnimation.Slide);
                }
            };

            _scrollView.AddSubview(_testButton2);
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

            AutoLayoutToolBox.AlignTopToBottomOf(_testButton1, _imageView);
            AutoLayoutToolBox.AlignLeftAnchorToLeftOf(_testButton1, _scrollView);
            AutoLayoutToolBox.AlignRightAnchorToRightOf(_testButton1, _scrollView);

            AutoLayoutToolBox.AlignTopToBottomOf(_testButton2, _testButton1);
            AutoLayoutToolBox.AlignLeftAnchorToLeftOf(_testButton2, _scrollView);
            AutoLayoutToolBox.AlignRightAnchorToRightOf(_testButton2, _scrollView);
        }
    }
}
