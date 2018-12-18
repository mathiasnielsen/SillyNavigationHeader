using UIKit;

namespace TestingTopHeader
{
    public class OuterViewController2 : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = this.GetType().Name + "Small navBar";

            View.BackgroundColor = UIColor.Blue;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.NavigationBar.PrefersLargeTitles = false;
            NavigationController?.SetNavigationBarHidden(false, true);
            NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Never;
        }
    }
}
