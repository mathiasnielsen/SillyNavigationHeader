using System;
using UIKit;

namespace TestingTopHeader
{
    public class ViewBase : UIViewController
    {
        private UIVisualEffectView _blurView;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetCustomTranslucentNavigationBarStyle();
        }

        private void SetCustomTranslucentNavigationBarStyle()
        {
            if (NavigationController != null && NavigationController.NavigationBar != null)
            {
                var blurEffect = UIBlurEffectStyle.Light;
                _blurView = new UIVisualEffectView(UIBlurEffect.FromStyle(blurEffect));
                View.AddSubview(_blurView);

                var navigationBar = NavigationController.NavigationBar;
                FullyConstraintTopLayouts(_blurView, navigationBar);
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            View.BringSubviewToFront(_blurView);
        }

        private void FullyConstraintTopLayouts(UIView subview, UINavigationBar navigationBar)
        {
            subview.TranslatesAutoresizingMaskIntoConstraints = false;
            subview.LeftAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.LeftAnchor).Active = true;
            subview.RightAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.RightAnchor).Active = true;
            subview.TopAnchor.ConstraintEqualTo(View.TopAnchor, -navigationBar.Frame.Y).Active = true;
            subview.BottomAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor).Active = true;
        }
    }
}
