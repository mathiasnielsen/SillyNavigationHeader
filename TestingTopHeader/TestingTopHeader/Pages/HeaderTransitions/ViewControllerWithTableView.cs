using System;
using Foundation;
using UIKit;

namespace TestingTopHeader
{
    public class ViewControllerWithTableView : UIViewController
    {
        private UITableView _tableView;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = "Tableview large header";

            _tableView = new UITableView();
            View.AddSubview(_tableView);

            AutoLayoutToolBox.AlignToFullConstraints(_tableView, View);


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

        private void Test()
        {
            if (NavigationItem != null)
            {
                NavigationItem.HidesBackButton = false;
            }
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
