using System;
using UIKit;

namespace TestingTopHeader
{
    public class InnerViewController3 : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = this.GetType().Name + "Big navbar";
            View.BackgroundColor = UIColor.Green;

            if (NavigationController != null)
            {
                NavigationController.NavigationBar.PrefersLargeTitles = true;
                NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Always;
            }
        }
    }
}
