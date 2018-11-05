using UIKit;

namespace TestingTopHeader
{
    public class OuterViewController4 : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = this.GetType().Name + "Auto navbar";

            View.BackgroundColor = UIColor.Yellow;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.NavigationBar.PrefersLargeTitles = true;
            NavigationController?.SetNavigationBarHidden(false, true);
            NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Automatic;
        }
    }
}
