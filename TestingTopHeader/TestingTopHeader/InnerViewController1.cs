using System;
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
    }
}
