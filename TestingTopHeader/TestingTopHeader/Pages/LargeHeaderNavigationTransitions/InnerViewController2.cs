using System;
using UIKit;

namespace TestingTopHeader
{
    public class InnerViewController2 : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = this.GetType().Name;
            View.BackgroundColor = UIColor.Brown;
        }
    }
}
