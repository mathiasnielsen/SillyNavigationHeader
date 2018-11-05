using System;
using UIKit;

namespace TestingTopHeader
{
    public class OuterViewController1 : UIViewController
    {
        private bool _hasAppeared;
        private UINavigationController _innerNavigationController;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = this.GetType().Name;

            View.BackgroundColor = UIColor.Purple;

            _innerNavigationController = new UINavigationController(new InnerViewController1());
            AddChildViewController(_innerNavigationController);
            _innerNavigationController.DidMoveToParentViewController(this);
            View.AddSubview(_innerNavigationController.View);

            _innerNavigationController.View.TranslatesAutoresizingMaskIntoConstraints = false;
            _innerNavigationController.View.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            _innerNavigationController.View.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;
            _innerNavigationController.View.LeftAnchor.ConstraintEqualTo(View.LeftAnchor).Active = true;
            _innerNavigationController.View.RightAnchor.ConstraintEqualTo(View.RightAnchor).Active = true;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            try
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                {
                    if (NavigationController != null && _hasAppeared == false)
                    {
                        _hasAppeared = true;

                        // NOTE! Calling this, when a viewcontroller of the inner navigationcontroller also has PreferLargeTitles true, will throw an exception.
                        NavigationController.NavigationBar.PrefersLargeTitles = false;
                        NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Automatic;
                    }

                    NavigationController?.SetNavigationBarHidden(true, true);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to manipulate NavigationBar, ex: {ex.Message}");
            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            // NOTE! In order to ensure, that outer viewcontroller navigations will not have issues with large titles.
            _innerNavigationController.NavigationBar.PrefersLargeTitles = false;
        }
    }
}
