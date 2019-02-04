using System;
using UIKit;

namespace TestingTopHeader
{
    public class OverviewView : ViewBase
    {
        private UIStackView _stackView;

        protected override void OnPrepareUIElements()
        {
            _stackView = new UIStackView()
            {
                Axis = UILayoutConstraintAxis.Vertical,
                Distribution = UIStackViewDistribution.EqualSpacing,
            };

            View.AddSubview(_stackView);
            AutoLayoutToolBox.AlignToFullConstraints(_stackView, View);

            var transitionButton = new UIButton(UIButtonType.System);
            transitionButton.SetTitle("Transition", UIControlState.Normal);
            transitionButton.TouchUpInside += (sender, e) =>
            {
                NavigationController.PushViewController(new InnerViewController1(), true);
            };

            _stackView.AddArrangedSubview(transitionButton);

            var translucentButton = new UIButton(UIButtonType.System);
            translucentButton.SetTitle("Translucent", UIControlState.Normal);
            translucentButton.TouchUpInside += (sender, e) =>
            {
                NavigationController.PushViewController(new Page1Translucent(), true);
            };

            _stackView.AddArrangedSubview(transitionButton);
        }
    }
}
