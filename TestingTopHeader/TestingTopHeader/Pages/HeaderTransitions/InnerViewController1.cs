using System;
using System.Runtime.CompilerServices;
using UIKit;

namespace TestingTopHeader
{
    public class InnerViewController1 : UIViewController
    {
        private UIScrollView _scrollView;
        private UISwitch _useAnimationSwitch;

        private int NavHeightBeforeLeavingView;

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            _scrollView.ContentSize = new CoreGraphics.CGSize(View.Frame.Width, View.Frame.Height + 20);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Title = this.GetType().Name;
            View.BackgroundColor = UIColor.Orange;

            InitializeScrollView();

            InitializeUseAnimationSwitch();

            InitializeInnerNav2Button();
            InitializeInnerNav3Button();
            InitializeInnerNav4Button();

            InitializeOuterNav2Button();
            InitializeOuterNav3Button();
            InitializeOuterNav4Button();

            InitializeTableViewButton();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            LogNavigationInfo();

            if (NavigationController != null)
            {
                NavigationController.NavigationBar.PrefersLargeTitles = true;
            }

            NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Automatic;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            LogNavigationInfo();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(true);
            LogNavigationInfo();

            NavHeightBeforeLeavingView = GetNavBarHeight();
            if (NavigationController.NavigationBar.Frame.Height > 50)
            {
                NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Always;
            }
            else
            {
                NavigationItem.LargeTitleDisplayMode = UINavigationItemLargeTitleDisplayMode.Never;
            }

            var coordinator = this.GetTransitionCoordinator();
            if (coordinator != null)
            {
                coordinator.AnimateAlongsideTransition(
                    animate:
                    (IUIViewControllerTransitionCoordinatorContext obj) =>
                    {
                        LogNavigationInfo("Animate");
                    },
                    completion:
                    (IUIViewControllerTransitionCoordinatorContext obj) =>
                    {
                        LogNavigationInfo("Completion");
                    });
            }
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            LogNavigationInfo();

            // NOTE! Navigating to an OuterViewController with PrefersLargeTitles = true, the current inner navigationcontroller cannot have the same property set to true.
            ////NavigationController.NavigationBar.PrefersLargeTitles = false;
        }

        private void LogNavigationInfo([CallerMemberName] string callerName = "")
        {
            System.Diagnostics.Debug.WriteLine($"Caller: {callerName}, Navigation bar height: {GetNavBarHeight()}");
        }

        private int GetNavBarHeight()
        {
            return (int)NavigationController.NavigationBar.Frame.Height;
        }

        private void InitializeUseAnimationSwitch()
        {
            _useAnimationSwitch = new UISwitch();
            _useAnimationSwitch.On = true;
            _scrollView.AddSubview(_useAnimationSwitch);

            _useAnimationSwitch.TranslatesAutoresizingMaskIntoConstraints = false;
            _useAnimationSwitch.TopAnchor.ConstraintEqualTo(_scrollView.TopAnchor, 50).Active = true;
            _useAnimationSwitch.CenterXAnchor.ConstraintEqualTo(_scrollView.CenterXAnchor).Active = true;
        }

        private void InitializeScrollView()
        {
            _scrollView = new UIScrollView();
            View.AddSubview(_scrollView);

            _scrollView.TranslatesAutoresizingMaskIntoConstraints = false;
            _scrollView.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            _scrollView.LeftAnchor.ConstraintEqualTo(View.LeftAnchor).Active = true;
            _scrollView.RightAnchor.ConstraintEqualTo(View.RightAnchor).Active = true;
            _scrollView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;
        }

        private void InitializeInnerNav2Button()
        {
            var button = CreteButton("Next page in inner small header", 100);
            button.TouchUpInside += (object sender, EventArgs e) =>
            {
                NavigationController?.PushViewController(new InnerViewController2(), true);
            };
        }

        private void InitializeInnerNav3Button()
        {
            var button = CreteButton("Next page in inner big header", 150);
            button.TouchUpInside += (object sender, EventArgs e) =>
            {
                NavigationController?.PushViewController(new InnerViewController3(), true);
            };
        }

        private void InitializeInnerNav4Button()
        {
            var button = CreteButton("Next page in inner auto header", 200);
            button.TouchUpInside += (object sender, EventArgs e) =>
            {
                NavigationController?.PushViewController(new InnerViewController4(), true);
            };
        }

        private void InitializeOuterNav2Button()
        {
            var button = CreteButton("Next page in outer smaller header", 250);
            button.TouchUpInside += (object sender, EventArgs e) =>
            {
                var parentNavController = (UINavigationController)ParentViewController.ParentViewController.ParentViewController;
                var useAnimation = _useAnimationSwitch.On;
                parentNavController?.PushViewController(new OuterViewController2(), useAnimation);
            };
        }

        private void InitializeOuterNav3Button()
        {
            var button = CreteButton("Next page in outer big header", 300);
            button.TouchUpInside += (object sender, EventArgs e) =>
            {
                var parentNavController = (UINavigationController)ParentViewController.ParentViewController.ParentViewController;
                var useAnimation = _useAnimationSwitch.On;
                parentNavController?.PushViewController(new OuterViewController3(), useAnimation);
            };
        }

        private void InitializeOuterNav4Button()
        {
            var button = CreteButton("Next page in outer auto header", 350);
            button.TouchUpInside += (object sender, EventArgs e) =>
            {
                var parentNavController = (UINavigationController)ParentViewController.ParentViewController.ParentViewController;
                var useAnimation = _useAnimationSwitch.On;
                parentNavController?.PushViewController(new OuterViewController4(), useAnimation);
            };
        }

        private void InitializeTableViewButton()
        {
            var button = CreteButton("To tableview viewcontroller with large header", 400);
            button.TouchUpInside += (object sender, EventArgs e) =>
            {
                NavigationController?.PushViewController(new ViewControllerWithTableView(), true);
            };
        }

        private UIButton CreteButton(string title, int top)
        {
            var button = new UIButton(UIButtonType.System);
            button.SetTitle(title, UIControlState.Normal);

            _scrollView.AddSubview(button);

            button.TranslatesAutoresizingMaskIntoConstraints = false;
            button.TopAnchor.ConstraintEqualTo(_scrollView.TopAnchor, top).Active = true;
            button.CenterXAnchor.ConstraintEqualTo(_scrollView.CenterXAnchor).Active = true;

            return button;
        }
    }
}
