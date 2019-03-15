using System;
using Foundation;
using UIKit;

namespace TestingTopHeader
{
    public class ViewControllerWithTableView : UIViewController, IUIGestureRecognizerDelegate
    {
        private UIView _contentView;
        private UITableView _tableView;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Tableview large header";

            _tableView = new UITableView();
            _contentView.AddSubview(_tableView);

            AutoLayoutToolBox.AlignToFullConstraints(_tableView, _contentView);


            var items = new string[]
            {
                "item 1",
                "item 2",
                "item 3",
                "item 4",
                "item 5",
            };

            var tableSource = new TableSource(items);
            _tableView.Source = tableSource;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (NavigationController != null)
            {
                ////NavigationController.NavigationBar.PrefersLargeTitles = true;
                NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Always;
            }

            Test();
        }

        private void InitializeContentView()
        {
            _contentView = new UIView();
            View.AddSubview(_contentView);

            _contentView.TranslatesAutoresizingMaskIntoConstraints = false;
            _contentView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor).Active = true;
            _contentView.LeftAnchor.ConstraintEqualTo(View.LeftAnchor).Active = true;
            _contentView.RightAnchor.ConstraintEqualTo(View.RightAnchor).Active = true;
            _contentView.BottomAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.BottomAnchor).Active = true;
        }

        private void Test()
        {
            if (NavigationController != null)
            {
                NavigationController.InteractivePopGestureRecognizer.Delegate = this;
            }

            if (NavigationItem != null)
            {
                NavigationItem.HidesBackButton = false;
            }

            // TODO Should be in disappear
            ////var isLargeTitleShown = NavigationController?.NavigationBar.Frame.Height > 44;
            ////if (isLargeTitleShown)
            ////{
            ////    NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Always;
            ////}
            ////else
            ////{
            ////    NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Never;
            ////}
        }

        private class TableSource : UITableViewSource
        {
            private const string CellIdentifier = nameof(CellIdentifier);

            private readonly string[] TableItems;

            public TableSource(string[] items)
            {
                TableItems = items;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return TableItems.Length;
            }

            public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
            {
                return 44;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
                string item = TableItems[indexPath.Row];

                //---- if there are no cells to reuse, create a new one
                if (cell == null)
                {
                    cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
                }

                cell.TextLabel.Text = item;

                return cell;
            }
        }
    }
}
