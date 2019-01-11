using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace LaUI.Behavior
{
    public class CenterInCanvas: Behavior<FrameworkElement>
    {
        #region Override

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }

        #endregion

        #region EventHandler

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            Canvas parent = VisualTreeHelper.GetParent(AssociatedObject) as Canvas;
            if (parent == null) return;
            double left = Math.Abs(AssociatedObject.ActualWidth - parent.ActualWidth) / 2;
            double top = Math.Abs(AssociatedObject.ActualHeight - parent.ActualHeight) / 2;
            Canvas.SetLeft(AssociatedObject, left);
            Canvas.SetTop(AssociatedObject, top);
        }

        #endregion
    }
}
