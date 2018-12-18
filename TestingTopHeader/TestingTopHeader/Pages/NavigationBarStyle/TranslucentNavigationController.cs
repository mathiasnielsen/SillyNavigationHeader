using System;
using UIKit;

namespace TestingTopHeader
{
    public class TranslucentNavigationController : UINavigationController
    {
        private UIVisualEffectView _blurView;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetCustomTranslucentNavigationBarStyle();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
        }

        private void SetCustomTranslucentNavigationBarStyle()
        {
            if (NavigationBar != null)
            {
                var blurEffect = UIBlurEffectStyle.Light;
                _blurView = new UIVisualEffectView(UIBlurEffect.FromStyle(blurEffect));

                NavigationBar.AddSubview(_blurView);
                NavigationBar.SendSubviewToBack(_blurView);
                FullyConstraintTopLayouts(_blurView, NavigationBar);
            }
        }

        private void FullyConstraintTopLayouts(UIView subview, UINavigationBar navigationBar)
        {
            // One way to find the current statusbar height
            // This is typically 20points, but 40 during a call, and 0 when hidden.
            var statusBarHeight = UIApplication.SharedApplication.StatusBarFrame.Size.Height;

            subview.TranslatesAutoresizingMaskIntoConstraints = false;
            subview.LeftAnchor.ConstraintEqualTo(navigationBar.LeftAnchor).Active = true;
            subview.RightAnchor.ConstraintEqualTo(navigationBar.RightAnchor).Active = true;

            // If the navigationbar is hidden, and revealed again, the blur will no longer be visible.
            ////subview.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;

            // But if we put iy like this, the navigationbar will work when revealed again (and also, cover the statusbar when navbar hidden)
            subview.TopAnchor.ConstraintEqualTo(navigationBar.TopAnchor, -statusBarHeight).Active = true;

            subview.BottomAnchor.ConstraintEqualTo(navigationBar.BottomAnchor).Active = true;
        }
    }
}
