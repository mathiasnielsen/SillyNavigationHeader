using UIKit;

namespace TestingTopHeader
{
    public class OuterViewController3 : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = this.GetType().Name + "Big navBar";

            View.BackgroundColor = UIColor.Cyan;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.NavigationBar.PrefersLargeTitles = true;
            NavigationController?.SetNavigationBarHidden(false, true);
            NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Always;
        }
    }
}
