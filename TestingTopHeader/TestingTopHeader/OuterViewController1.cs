using System;
using UIKit;

namespace TestingTopHeader
{
    public class OuterViewController1 : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = this.GetType().Name;

            View.BackgroundColor = UIColor.Purple;

            var innerNavController = new UINavigationController(new InnerViewController1());
            AddChildViewController(innerNavController);
            innerNavController.DidMoveToParentViewController(this);
            View.AddSubview(innerNavController.View);

            innerNavController.View.TranslatesAutoresizingMaskIntoConstraints = false;
            innerNavController.View.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            innerNavController.View.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;
            innerNavController.View.LeftAnchor.ConstraintEqualTo(View.LeftAnchor).Active = true;
            innerNavController.View.RightAnchor.ConstraintEqualTo(View.RightAnchor).Active = true;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController?.SetNavigationBarHidden(true, true);
        }
    }
}
