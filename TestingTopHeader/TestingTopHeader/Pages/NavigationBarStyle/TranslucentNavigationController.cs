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
            //SetCustomTranslucentNavigationBarStyle();
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

        public override void PushViewController(UIViewController viewController, bool animated)
        {
            base.PushViewController(viewController, animated);
        }

        private void FullyConstraintTopLayouts(UIView subview, UINavigationBar navigationBar)
        {
            subview.TranslatesAutoresizingMaskIntoConstraints = false;
            subview.LeftAnchor.ConstraintEqualTo(navigationBar.LeftAnchor).Active = true;
            subview.RightAnchor.ConstraintEqualTo(navigationBar.RightAnchor).Active = true;
            subview.TopAnchor.ConstraintEqualTo(navigationBar.TopAnchor, -navigationBar.Frame.Y).Active = true;
            subview.BottomAnchor.ConstraintEqualTo(navigationBar.BottomAnchor).Active = true;
        }
    }
}
