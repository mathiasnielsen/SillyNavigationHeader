using System;
using CoreGraphics;
using UIKit;

namespace TestingTopHeader
{
    public class InnerViewController1 : UIViewController
    {
        private UIScrollView _scrollView;

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            _scrollView.ContentSize = new CoreGraphics.CGSize(View.Frame.Width, View.Frame.Height + 20);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (NavigationController != null)
            {
                NavigationController.NavigationBar.PrefersLargeTitles = true;
                NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Always;
            }

            Title = this.GetType().Name;
            View.BackgroundColor = UIColor.Orange;

            InitializeScrollView();
            InitializeInnerNavButton();
            InitializeOuterNavButton();

            InitializeSlider();
        }

        private void InitializeScrollView()
        {
            _scrollView = new UIScrollView();
            View.AddSubview(_scrollView);

            _scrollView.TranslatesAutoresizingMaskIntoConstraints = false;
            _scrollView.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            _scrollView.LeftAnchor.ConstraintEqualTo(View.LeftAnchor).Active = true;
            _scrollView.RightAnchor.ConstraintEqualTo(View.RightAnchor).Active = true;
            _scrollView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;
        }

        private void InitializeInnerNavButton()
        {
            var button = new UIButton(UIButtonType.System);
            button.SetTitle("Next page in inner", UIControlState.Normal);
            button.TouchUpInside += (object sender, EventArgs e) =>
            {
                NavigationController?.PushViewController(new InnerViewController2(), true);
            };

            _scrollView.AddSubview(button);

            button.TranslatesAutoresizingMaskIntoConstraints = false;
            button.TopAnchor.ConstraintEqualTo(_scrollView.TopAnchor, 100).Active = true;
            button.CenterXAnchor.ConstraintEqualTo(_scrollView.CenterXAnchor).Active = true;
        }

        private void InitializeOuterNavButton()
        {
            var button = new UIButton(UIButtonType.System);
            button.SetTitle("Next page in outer", UIControlState.Normal);
            button.TouchUpInside += (object sender, EventArgs e) =>
            {
                var parentNavController = (UINavigationController)ParentViewController.ParentViewController.ParentViewController;
                parentNavController?.PushViewController(new OuterViewController2(), true);
            };

            _scrollView.AddSubview(button);

            button.TranslatesAutoresizingMaskIntoConstraints = false;
            button.TopAnchor.ConstraintEqualTo(_scrollView.TopAnchor, 200).Active = true;
            button.CenterXAnchor.ConstraintEqualTo(_scrollView.CenterXAnchor).Active = true;
        }

        private void InitializeSlider()
        {
            var slider = new SteppedSlider();

            _scrollView.AddSubview(slider);

            slider.TranslatesAutoresizingMaskIntoConstraints = false;
            slider.TopAnchor.ConstraintEqualTo(_scrollView.TopAnchor, 300).Active = true;
            slider.LeftAnchor.ConstraintEqualTo(_scrollView.LeftAnchor, 40).Active = true;
            slider.HeightAnchor.ConstraintEqualTo(100).Active = true;
            slider.WidthAnchor.ConstraintEqualTo(300).Active = true;

            slider.ValueChanged += OnSliderChanged;
        }

        private void OnSliderChanged(object sender, EventArgs e)
        {
            var slider = (SteppedSlider)sender;
            var slideValue = slider.Value;

            var isFirst = slideValue < 0.5;
            if (isFirst)
            {
                slider.Value = 0;
                ////return;
            }

            var isLast = slideValue > slider.MaxValue - 0.5;
            if (isLast)
            {
                slider.Value = slider.MaxValue;
                ////return;
            }

            var roundedValue = Math.Round(slideValue);
            var roundedValueInProcentages = roundedValue / slider.MaxValue;
            var actualSlideValue = slider.MaxValue * roundedValueInProcentages;

            // EXperiment
            var lengthOfThumb = 40;
            ////var deltaFromMid = actualSlideValue - (roundedValueInProcentages * lengthOfThumb);

            var length = slider.MaxValue - slider.MinValue;
            var mid = length / 2;
            var deltaFromMid = roundedValue - mid;
            var deltaInProcentages = deltaFromMid / length;

            var adjustment = lengthOfThumb * deltaInProcentages;
            var thumbSliderWidth = slider.Frame.Width - lengthOfThumb;
            var adjustmentInProcentages = adjustment / thumbSliderWidth;
            var adjustmentMultiplier = 1.0 + adjustmentInProcentages;

            ////var finalValue = (float)actualSlideValue * (float)adjustmentMultiplier;
            var finalValue = (float)actualSlideValue * (float)adjustmentMultiplier;

            System.Diagnostics.Debug.WriteLine(
                "Actual value: " + slider.Value + Environment.NewLine +
                ", Delta mid: " + deltaFromMid + Environment.NewLine +
                ", Delta progress: " + deltaInProcentages + Environment.NewLine +
                ", AdjustmentInProcentages: " + adjustmentInProcentages + Environment.NewLine +
                ", AdjustmentMultiplier: " + adjustmentMultiplier + Environment.NewLine +
                ", Rounded value in per: " + roundedValueInProcentages + Environment.NewLine +
                ", Final value: " + finalValue);

            slider.Value = finalValue;
        }

        private void NewVersion(SteppedSlider slider)
        {
            var sliderValue = slider.Value;

            var steps = 4;
            var normalStepLength = slider.Frame.Width / steps;
            var thumbWidth = 40;

            if (sliderValue > 0.5)
            {
            }
        }
    }

    public class SteppedSlider : UISlider
    {
        private const int ThumbRadius = 5;

        public SteppedSlider()
        {
            MaxValue = 4;
            MinValue = 0;

            NumberOfTicks = 5;
            TickWidth = 1;
            TickHeight = 10;
            TickColor = UIColor.Black;
            TrackColor = UIColor.Black;
        }

        public int NumberOfTicks { get; set; }

        public int TickWidth { get; set; }

        public int TickHeight { get; set; }

        public UIColor TickColor { get; set; }

        public UIColor TrackColor { get; set; }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            var innerRect = rect.Inset(2.0f, TickHeight);

            UIGraphics.BeginImageContextWithOptions(innerRect.Size, false, 0);
            var context = UIGraphics.GetCurrentContext();

            var selectedSide = GetTrackImage(innerRect, context);
            var selectedStripSide = GetTrackWithTicksImage(innerRect, context, selectedSide);

            var unselectedSide = GetTrackImage(innerRect, context);
            var unselectedStripSide = GetTrackWithTicksImage(innerRect, context, unselectedSide);

            UIGraphics.EndImageContext();

            SetMinTrackImage(selectedStripSide, UIControlState.Normal);
            SetMaxTrackImage(unselectedStripSide, UIControlState.Normal);
        }

        private UIImage GetTrackImage(CGRect innerRect, CGContext context)
        {
            context.SetLineCap(CGLineCap.Round);
            context.SetLineWidth(1);
            context.MoveTo(1, innerRect.Height / 2);
            context.AddLineToPoint(innerRect.Width - 1, innerRect.Height / 2);
            context.SetStrokeColor(TrackColor.CGColor);
            context.StrokePath();

            return UIGraphics.GetImageFromCurrentImageContext().CreateResizableImage(UIEdgeInsets.Zero);
        }

        private UIImage GetTrackWithTicksImage(CGRect innerRect, CGContext context, UIImage side)
        {
            var tickStartPointY = (innerRect.Height / 2) - (TickHeight / 2);
            var tickEndPointY = (innerRect.Height / 2) + (TickHeight / 2);

            side.Draw(new CGPoint(0, 0));
            for (int i = 0; i < NumberOfTicks; i++)
            {
                context.SetLineWidth(TickWidth);

                var position = (innerRect.Size.Width / (NumberOfTicks - 1)) * i;

                if (i == 0)
                {
                    position += 1;
                }
                else if (i == NumberOfTicks - 1)
                {
                    position -= 1;
                }

                context.MoveTo(position, tickStartPointY);
                context.AddLineToPoint(position, tickEndPointY);

                context.SetStrokeColor(TickColor.CGColor);
                context.StrokePath();
            }

            return UIGraphics.GetImageFromCurrentImageContext().CreateResizableImage(UIEdgeInsets.Zero);
        }
    }
}
