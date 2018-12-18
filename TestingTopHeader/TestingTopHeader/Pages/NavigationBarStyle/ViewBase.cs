using System;
using UIKit;

namespace TestingTopHeader
{
    public abstract class ViewBase : UIViewController
    {
        private UIVisualEffectView _blurView;

        public override sealed void ViewDidLoad()
        {
            base.ViewDidLoad();
            PrepareUIElements();
            SetCustomTranslucentNavigationBarStyle();
        }

        private void PrepareUIElements()
        {
            OnPrepareUIElements();
        }

        protected abstract void OnPrepareUIElements();

        private void SetCustomTranslucentNavigationBarStyle()
        {
            // The issues about setting it here, on each view, is that the BounceEffect will not work.
            if (NavigationController != null && NavigationController.NavigationBar != null)
            {
                var blurEffect = UIBlurEffectStyle.Light;
                _blurView = new UIVisualEffectView(UIBlurEffect.FromStyle(blurEffect));
                View.AddSubview(_blurView);

                var navigationBar = NavigationController.NavigationBar;
                FullyConstraintTopLayouts(_blurView, navigationBar);
                View.BringSubviewToFront(_blurView);
            }
        }

        private void FullyConstraintTopLayouts(UIView subview, UINavigationBar navigationBar)
        {
            subview.TranslatesAutoresizingMaskIntoConstraints = false;
            subview.LeftAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.LeftAnchor).Active = true;
            subview.RightAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.RightAnchor).Active = true;

            subview.TopAnchor.ConstraintEqualTo(View.TopAnchor, -navigationBar.Frame.Y).Active = true;

            // This constraint makes it look nice, but not when bounce dragging.
            subview.BottomAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor).Active = true;

            // This constraint is unreachable to the subview; an error will occur.
            ////subview.BottomAnchor.ConstraintEqualTo(navigationBar.BottomAnchor).Active = true;
        }
    }
}
