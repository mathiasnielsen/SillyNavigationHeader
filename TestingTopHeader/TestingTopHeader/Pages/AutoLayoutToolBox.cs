using System;
using UIKit;

namespace TestingTopHeader
{
    public static class AutoLayoutToolBox
    {
        public static NSLayoutConstraint AlignTopToBottomOf(UIView view, UIView otherView)
        {
            view.TranslatesAutoresizingMaskIntoConstraints = false;
            var constraint = view.TopAnchor.ConstraintEqualTo(otherView.BottomAnchor);
            constraint.Active = true;

            return constraint;
        }

        public static NSLayoutConstraint AlignLeftAnchorToLeftOf(UIView view, UIView otherView)
        {
            view.TranslatesAutoresizingMaskIntoConstraints = false;
            var constraint = view.LeftAnchor.ConstraintEqualTo(otherView.LeftAnchor);
            constraint.Active = true;

            return constraint;
        }

        public static NSLayoutConstraint AlignRightAnchorToRightOf(UIView view, UIView otherView)
        {
            view.TranslatesAutoresizingMaskIntoConstraints = false;
            var constraint = view.RightAnchor.ConstraintEqualTo(otherView.RightAnchor);
            constraint.Active = true;

            return constraint;
        }

        public static NSLayoutConstraint AlignTopAnchorTopOf(UIView view, UIView otherView)
        {
            view.TranslatesAutoresizingMaskIntoConstraints = false;
            var constraint = view.TopAnchor.ConstraintEqualTo(otherView.TopAnchor);
            constraint.Active = true;

            return constraint;
        }

        public static NSLayoutConstraint AlignBottomAnchorToBottomOf(UIView view, UIView otherView)
        {
            view.TranslatesAutoresizingMaskIntoConstraints = false;
            var constraint = view.BottomAnchor.ConstraintEqualTo(otherView.BottomAnchor);
            constraint.Active = true;

            return constraint;
        }

        public static NSLayoutConstraint[] AlignToFullConstraints(UIView view, UIView otherView)
        {
            view.TranslatesAutoresizingMaskIntoConstraints = false;

            return new NSLayoutConstraint[]
            {
                AlignLeftAnchorToLeftOf(view, otherView),
                AlignTopAnchorTopOf(view, otherView),
                AlignRightAnchorToRightOf(view, otherView),
                AlignBottomAnchorToBottomOf(view, otherView),
            };
        }
    }
}
