﻿using UIKit;

namespace TestingTopHeader
{
    public class InnerViewController2 : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = this.GetType().Name + "Small navbar";
            View.BackgroundColor = UIColor.Brown;

            if (NavigationController != null)
            {
                NavigationController.NavigationBar.PrefersLargeTitles = true;
                NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Never;
            }
        }
    }
}
