using System.Windows;

namespace LaUI.Controls.Helper
{
    public static class ControlsHelper
    {

        public static CornerRadius GetCornerRadius(UIElement obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(UIElement obj, CornerRadius value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for CornerRadiusProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached(
                "CornerRadius", typeof(CornerRadius), 
                typeof(ControlsHelper), 
                new FrameworkPropertyMetadata(
                    new CornerRadius(0),
                    FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
    }
}
