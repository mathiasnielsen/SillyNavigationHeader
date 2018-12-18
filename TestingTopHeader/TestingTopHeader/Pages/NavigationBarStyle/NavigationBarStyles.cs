using System;
using CoreGraphics;
using CoreImage;
using UIKit;

namespace TestingTopHeader
{
    public static class NavigationBarStyles
    {
        public static void SetDefaultAppearance()
        {
            // The actual translucency.
            UINavigationBar.Appearance.Translucent = true;

            // Removes the line below the navigationbar
            UINavigationBar.Appearance.ShadowImage = new UIImage();

            // Changes the color of the buttons
            UINavigationBar.Appearance.TintColor = UIColor.Magenta;

            TranslucentNoBackground();
        }

        private static void TranslucentWithWhiteBackgroundColor()
        {
            // Makes the color of the translucent bar "whiter" (not entirely white)
            // Also, loses a lot of the translucent effect, compared to using clear.
            UINavigationBar.Appearance.BackgroundColor = UIColor.White;
        }

        private static void TranslucentWithBackgroundColor()
        {
            // Makes the color of the translucent bar "whiter" (not entirely white)
            // Also, loses a lot of the translucent effect, compared to using clear.
            UINavigationBar.Appearance.BackgroundColor = UIColor.Orange;
        }

        private static void TranslucentWithDifferentBarStyle()
        {
            UINavigationBar.Appearance.BarStyle = UIBarStyle.Default;
        }

        private static void TranslucentButWithAnAcutalColor()
        {
            // Breaks translucent effect
            UINavigationBar.Appearance.BarTintColor = UIColor.Orange;
        }

        private static void TranslucentCustomBlurredImageAsBackground()
        {
            var image = UIImage.FromFile("Assets/test_image.png");
            var blurredImage = BlurImage(image, 5);
            UINavigationBar.Appearance.SetBackgroundImage(blurredImage, UIBarMetrics.Default);
        }

        private static void TranslucentCustomImageAsBackground()
        {
            var backgroundImage = ImageFromColor(UIColor.Orange, CGRect.Empty);
            UINavigationBar.Appearance.SetBackgroundImage(backgroundImage, UIBarMetrics.Default);

            // This is the effect, but this cannot be set as background image, since it is not an image.
            ////var blurEffect = UIBlurEffectStyle.Light;
            ////var blurView = new UIVisualEffectView(UIBlurEffect.FromStyle(blurEffect));
        }

        private static void TranslucentNoBackground()
        {
            // Remove any background
            // Also, it removes the line made by the shadow image.
            UINavigationBar.Appearance.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
        }

        public static UIImage ImageFromColor(UIColor color, CGRect frame)
        {
            var rect = frame == CGRect.Empty ? new CGRect(0, 0, 1, 1) : frame;
            UIGraphics.BeginImageContext(rect.Size);

            var ctx = UIGraphics.GetCurrentContext();
            ctx.SetFillColor(color.CGColor);
            ctx.FillRect(rect);

            var img = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            return img;
        }

        private static UIImage BlurImage(UIImage image, int blurRadius)
        {
            var beginImage = new CIImage(image);
            var blur = new CIGaussianBlur();
            blur.Image = beginImage;
            blur.Radius = blurRadius;

            var outputImage = blur.OutputImage;
            var context = CIContext.FromOptions(
                            new CIContextOptions() { UseSoftwareRenderer = true });

            // Orignal
            ////var cgImage = context.CreateCGImage(
            ////outputImage,
            ////new CGRect(new CGPoint(0, 0), image.Size));

            // Modified
            var cgImage = context.CreateCGImage(
                outputImage,
                new CGRect(new CGPoint(blurRadius, -blurRadius), image.Size));

            var newImage = UIImage.FromImage(cgImage);

            // Clear up resources.
            beginImage = null;
            context = null;
            blur = null;
            outputImage = null;
            cgImage = null;

            return newImage;
        }
    }
}
