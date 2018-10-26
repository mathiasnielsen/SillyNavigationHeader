using UIKit;

namespace TestingTopHeader
{
    public class InnerViewController4 : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = this.GetType().Name + "Auto navbar";
            View.BackgroundColor = UIColor.Brown;

            if (NavigationController != null)
            {
                NavigationController.NavigationBar.PrefersLargeTitles = true;
                NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Automatic;
            }
        }
    }
}
