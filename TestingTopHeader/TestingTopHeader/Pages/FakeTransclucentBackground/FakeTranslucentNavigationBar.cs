using System;
using System.Collections.Generic;
using UIKit;

namespace TestingTopHeader
{
    public class FakeTranslucentNavigationBar : UIViewController
    {
        private const int HeightOfNavBars = 50;
        private const int SpacingBetweenNavBars = 2;

        private static UIColor SeaShellColor = UIColor.FromRGB(241, 241, 241);

        private readonly UIColor TranslucentBackgroundColor = SeaShellColor.ColorWithAlpha(0.3f);
        private readonly UIColor ViewBackgroundColor = SeaShellColor.ColorWithAlpha(0.95f);

        private UIScrollView _scrollView;
        private UILabel _titleLabel;
        private UIImageView _imageView;
        private UIButton _nextButton;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = SeaShellColor;

            if (NavigationController.NavigationBar != null)
            {
                NavigationController.NavigationBar.Translucent = true;
                NavigationController.NavigationBar.ShadowImage = new UIImage();
                NavigationController.NavigationBar.BarTintColor = SeaShellColor;
            }

            InitializeUIElements();
            InitializeFakeNavBarNoVibrancy();
            InitializeFakeNavBar();

            SetupLayoutConstraints();
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            _scrollView.LayoutIfNeeded();
            _scrollView.ContentSize = new CoreGraphics.CGSize(0, View.Frame.Size.Height * 2);
        }

        private void InitializeFakeNavBarNoVibrancy()
        {
            var blurEffect = UIBlurEffect.FromStyle(UIBlurEffectStyle.Light);
            var blurEffectView = new UIVisualEffectView(blurEffect)
            {
                ////BackgroundColor = seaShellColor.ColorWithAlpha(0.9f),
            };

            var plainWhite = new UIView()
            {
                BackgroundColor = ViewBackgroundColor,
            };

            View.AddSubview(blurEffectView);
            blurEffectView.ContentView.Add(plainWhite);

            blurEffectView.TranslatesAutoresizingMaskIntoConstraints = false;
            blurEffectView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor, SpacingBetweenNavBars).Active = true;
            blurEffectView.LeftAnchor.ConstraintEqualTo(View.LeftAnchor).Active = true;
            blurEffectView.RightAnchor.ConstraintEqualTo(View.RightAnchor).Active = true;
            blurEffectView.HeightAnchor.ConstraintEqualTo(HeightOfNavBars).Active = true;

            plainWhite.TranslatesAutoresizingMaskIntoConstraints = false;
            plainWhite.TopAnchor.ConstraintEqualTo(blurEffectView.ContentView.TopAnchor).Active = true;
            plainWhite.LeftAnchor.ConstraintEqualTo(blurEffectView.ContentView.LeftAnchor).Active = true;
            plainWhite.RightAnchor.ConstraintEqualTo(blurEffectView.ContentView.RightAnchor).Active = true;
            plainWhite.BottomAnchor.ConstraintEqualTo(blurEffectView.ContentView.BottomAnchor).Active = true;
        }

        private void InitializeFakeNavBar()
        {
            var blurEffect = UIBlurEffect.FromStyle(UIBlurEffectStyle.Regular);
            var blurEffectView = new UIVisualEffectView(blurEffect)
            {
                ////BackgroundColor = seaShellColor.ColorWithAlpha(0.9f),
            };

            var vibrancyEffect = UIVibrancyEffect.FromBlurEffect(blurEffect);
            var vibrancyEffectView = new UIVisualEffectView(vibrancyEffect);
            var plainWhite = new UIView()
            {
                BackgroundColor = ViewBackgroundColor,
            };

            View.AddSubview(blurEffectView);
            blurEffectView.ContentView.Add(vibrancyEffectView);
            vibrancyEffectView.ContentView.Add(plainWhite);

            blurEffectView.TranslatesAutoresizingMaskIntoConstraints = false;
            var index = 1;
            var topOffset = (HeightOfNavBars * index) +  (SpacingBetweenNavBars * (index + 1));
            blurEffectView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor, topOffset).Active = true;
            blurEffectView.LeftAnchor.ConstraintEqualTo(View.LeftAnchor).Active = true;
            blurEffectView.RightAnchor.ConstraintEqualTo(View.RightAnchor).Active = true;
            blurEffectView.HeightAnchor.ConstraintEqualTo(HeightOfNavBars).Active = true;

            vibrancyEffectView.TranslatesAutoresizingMaskIntoConstraints = false;
            vibrancyEffectView.TopAnchor.ConstraintEqualTo(blurEffectView.ContentView.TopAnchor).Active = true;
            vibrancyEffectView.LeftAnchor.ConstraintEqualTo(blurEffectView.ContentView.LeftAnchor).Active = true;
            vibrancyEffectView.RightAnchor.ConstraintEqualTo(blurEffectView.ContentView.RightAnchor).Active = true;
            vibrancyEffectView.BottomAnchor.ConstraintEqualTo(blurEffectView.ContentView.BottomAnchor).Active = true;

            plainWhite.TranslatesAutoresizingMaskIntoConstraints = false;
            plainWhite.TopAnchor.ConstraintEqualTo(vibrancyEffectView.ContentView.TopAnchor).Active = true;
            plainWhite.LeftAnchor.ConstraintEqualTo(vibrancyEffectView.ContentView.LeftAnchor).Active = true;
            plainWhite.RightAnchor.ConstraintEqualTo(vibrancyEffectView.ContentView.RightAnchor).Active = true;
            plainWhite.BottomAnchor.ConstraintEqualTo(vibrancyEffectView.ContentView.BottomAnchor).Active = true;
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
                Text = "Testing the fake navigationbar translucency",
            };

            _nextButton = new UIButton(UIButtonType.System);
            _nextButton.SetTitle("Next", UIControlState.Normal);

            _scrollView.AddSubview(_titleLabel);
        }

        private void SetupLayoutConstraints()
        {
            AutoLayoutToolBox.AlignToFullConstraints(_scrollView, View);

            AutoLayoutToolBox.AlignLeftAnchorToLeftOf(_titleLabel, _scrollView);
            AutoLayoutToolBox.AlignRightAnchorToRightOf(_titleLabel, _scrollView);
            _titleLabel.TopAnchor.ConstraintEqualTo(_scrollView.TopAnchor, 600).Active = true;

            AutoLayoutToolBox.AlignLeftAnchorToLeftOf(_imageView, View);
            AutoLayoutToolBox.AlignRightAnchorToRightOf(_imageView, View);
            AutoLayoutToolBox.AlignTopToBottomOf(_imageView, _titleLabel);
        }
    }
}
