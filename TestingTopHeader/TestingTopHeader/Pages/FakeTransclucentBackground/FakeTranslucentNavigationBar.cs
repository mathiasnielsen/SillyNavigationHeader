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
        private static UIColor ThemeColor = SeaShellColor;

        private readonly UIColor TranslucentBackgroundColor = ThemeColor;
        private readonly UIColor ViewBackgroundColor = ThemeColor;

        private UIScrollView _scrollView;
        private UILabel _titleLabel;
        private UIImageView _imageView;
        private UIButton _nextButton;
        private UILabel _valueLabel;

        private UISlider _changeBlurViewAlphaSlider;
        private UISlider _changePlainViewAlphaSlider;

        private UIVisualEffectView _blurView01;
        private UIVisualEffectView _blurView02;

        private UIView _plainView01;
        private UIView _plainView02;

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
            _scrollView.ContentSize = new CoreGraphics.CGSize(0, View.Frame.Size.Height * 3);
        }

        private void InitializeFakeNavBarNoVibrancy()
        {
            var blurEffect = UIBlurEffect.FromStyle(UIBlurEffectStyle.Light);
            _blurView01 = new UIVisualEffectView(blurEffect)
            {
                ////BackgroundColor = seaShellColor.ColorWithAlpha(0.9f),
            };

            _plainView01 = new UIView()
            {
                BackgroundColor = ViewBackgroundColor,
            };

            View.AddSubview(_blurView01);
            _blurView01.ContentView.Add(_plainView01);

            _blurView01.TranslatesAutoresizingMaskIntoConstraints = false;
            _blurView01.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor, SpacingBetweenNavBars).Active = true;
            _blurView01.LeftAnchor.ConstraintEqualTo(View.LeftAnchor).Active = true;
            _blurView01.RightAnchor.ConstraintEqualTo(View.RightAnchor).Active = true;
            _blurView01.HeightAnchor.ConstraintEqualTo(HeightOfNavBars).Active = true;

            _plainView01.TranslatesAutoresizingMaskIntoConstraints = false;
            _plainView01.TopAnchor.ConstraintEqualTo(_blurView01.ContentView.TopAnchor).Active = true;
            _plainView01.LeftAnchor.ConstraintEqualTo(_blurView01.ContentView.LeftAnchor).Active = true;
            _plainView01.RightAnchor.ConstraintEqualTo(_blurView01.ContentView.RightAnchor).Active = true;
            _plainView01.BottomAnchor.ConstraintEqualTo(_blurView01.ContentView.BottomAnchor).Active = true;
        }

        private void InitializeFakeNavBar()
        {
            var blurEffect = UIBlurEffect.FromStyle(UIBlurEffectStyle.Regular);
            _blurView02 = new UIVisualEffectView(blurEffect)
            {
                ////BackgroundColor = seaShellColor.ColorWithAlpha(0.9f),
            };

            var vibrancyEffect = UIVibrancyEffect.FromBlurEffect(blurEffect);
            var vibrancyEffectView = new UIVisualEffectView(vibrancyEffect);
            _plainView02 = new UIView()
            {
                BackgroundColor = ViewBackgroundColor,
            };

            View.AddSubview(_blurView02);
            _blurView02.ContentView.Add(vibrancyEffectView);
            vibrancyEffectView.ContentView.Add(_plainView02);

            _blurView02.TranslatesAutoresizingMaskIntoConstraints = false;
            var index = 1;
            var topOffset = (HeightOfNavBars * index) +  (SpacingBetweenNavBars * (index + 1));
            _blurView02.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor, topOffset).Active = true;
            _blurView02.LeftAnchor.ConstraintEqualTo(View.LeftAnchor).Active = true;
            _blurView02.RightAnchor.ConstraintEqualTo(View.RightAnchor).Active = true;
            _blurView02.HeightAnchor.ConstraintEqualTo(HeightOfNavBars).Active = true;

            vibrancyEffectView.TranslatesAutoresizingMaskIntoConstraints = false;
            vibrancyEffectView.TopAnchor.ConstraintEqualTo(_blurView02.ContentView.TopAnchor).Active = true;
            vibrancyEffectView.LeftAnchor.ConstraintEqualTo(_blurView02.ContentView.LeftAnchor).Active = true;
            vibrancyEffectView.RightAnchor.ConstraintEqualTo(_blurView02.ContentView.RightAnchor).Active = true;
            vibrancyEffectView.BottomAnchor.ConstraintEqualTo(_blurView02.ContentView.BottomAnchor).Active = true;

            _plainView02.TranslatesAutoresizingMaskIntoConstraints = false;
            _plainView02.TopAnchor.ConstraintEqualTo(vibrancyEffectView.ContentView.TopAnchor).Active = true;
            _plainView02.LeftAnchor.ConstraintEqualTo(vibrancyEffectView.ContentView.LeftAnchor).Active = true;
            _plainView02.RightAnchor.ConstraintEqualTo(vibrancyEffectView.ContentView.RightAnchor).Active = true;
            _plainView02.BottomAnchor.ConstraintEqualTo(vibrancyEffectView.ContentView.BottomAnchor).Active = true;
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
            View.AddSubview(_nextButton);

            _valueLabel = new UILabel();
            View.AddSubview(_valueLabel);

            _changePlainViewAlphaSlider = new UISlider();
            _changePlainViewAlphaSlider.MinValue = 0.0f;
            _changePlainViewAlphaSlider.MaxValue = 1.0f;
            _changePlainViewAlphaSlider.ValueChanged += (sender, e) =>
            {
                var color = ViewBackgroundColor.ColorWithAlpha(_changePlainViewAlphaSlider.Value);
                _valueLabel.Text = _changePlainViewAlphaSlider.Value.ToString();

                _plainView01.BackgroundColor = color;
                _plainView02.BackgroundColor = color;
            };

            View.AddSubview(_changePlainViewAlphaSlider);

            _changeBlurViewAlphaSlider = new UISlider();
            _changeBlurViewAlphaSlider.MinValue = 0.0f;
            _changeBlurViewAlphaSlider.MaxValue = 1.0f;
            _changeBlurViewAlphaSlider.ValueChanged += (sender, e) =>
            {
                var color = ViewBackgroundColor.ColorWithAlpha(_changeBlurViewAlphaSlider.Value);
                _valueLabel.Text = _changeBlurViewAlphaSlider.Value.ToString();

                _blurView01.BackgroundColor = color;
                _blurView02.BackgroundColor = color;
            };

            View.AddSubview(_changeBlurViewAlphaSlider);

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

            AutoLayoutToolBox.AlignLeftAnchorToLeftOf(_valueLabel, View);
            AutoLayoutToolBox.AlignRightAnchorToRightOf(_valueLabel, View);
            _valueLabel.BottomAnchor.ConstraintEqualTo(_changePlainViewAlphaSlider.TopAnchor).Active = true;

            _changeBlurViewAlphaSlider.TranslatesAutoresizingMaskIntoConstraints = false;
            _changeBlurViewAlphaSlider.LeftAnchor.ConstraintEqualTo(View.LeftAnchor, 24).Active = true;
            _changeBlurViewAlphaSlider.RightAnchor.ConstraintEqualTo(View.RightAnchor, -24).Active = true;
            _changeBlurViewAlphaSlider.BottomAnchor.ConstraintEqualTo(_changePlainViewAlphaSlider.TopAnchor, -24).Active = true;
            _changeBlurViewAlphaSlider.HeightAnchor.ConstraintEqualTo(100).Active = true;
            _changeBlurViewAlphaSlider.LeftAnchor.ConstraintEqualTo(View.LeftAnchor, 24).Active = true;
            _changePlainViewAlphaSlider.TranslatesAutoresizingMaskIntoConstraints = false;
            _changePlainViewAlphaSlider.LeftAnchor.ConstraintEqualTo(View.LeftAnchor, 24).Active = true;
            _changePlainViewAlphaSlider.RightAnchor.ConstraintEqualTo(View.RightAnchor, -24).Active = true;
            _changePlainViewAlphaSlider.BottomAnchor.ConstraintEqualTo(View.BottomAnchor, -24).Active = true;
            _changePlainViewAlphaSlider.HeightAnchor.ConstraintEqualTo(100).Active = true;
        }
    }
}
