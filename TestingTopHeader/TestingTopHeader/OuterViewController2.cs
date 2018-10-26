using UIKit;

namespace TestingTopHeader
{
    public class OuterViewController2 : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = this.GetType().Name;

            View.BackgroundColor = UIColor.Blue;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            NavigationController?.SetNavigationBarHidden(false, true);
        }
    }
}
