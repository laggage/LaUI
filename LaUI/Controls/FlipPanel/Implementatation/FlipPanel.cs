using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace LaUI.Control
{
    [TemplatePart(Name = "FlipButton", Type = typeof(ToggleButton))]
    [TemplateVisualState(Name = "Normal", GroupName = "ViewStates")]
    [TemplateVisualState(Name = "Flipped", GroupName = "ViewStates")]
    public class FlipPanel : System.Windows.Controls.Control
    {
        static FlipPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlipPanel),
                new FrameworkPropertyMetadata(typeof(FlipPanel)));
        }

        #region DependencyProperty

        #region FrontContent

        public object FrontContent
        {
            get => GetValue(FrontContentProperty);
            set => SetValue(FrontContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for FrontContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FrontContentProperty =
            DependencyProperty.Register(nameof(FrontContent), typeof(object), typeof(FlipPanel),
                new FrameworkPropertyMetadata(null));

        #endregion

        #region BackContent

        public object BackContent
        {
            get => GetValue(BackContentProperty);
            set => SetValue(BackContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for BackContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackContentProperty =
            DependencyProperty.Register(nameof(BackContent), typeof(object), typeof(FlipPanel),
                new FrameworkPropertyMetadata(null));

        #endregion

        #region IsFlipped

        public bool IsFlipped
        {
            get => (bool)GetValue(IsFlippedProperty);
            set => SetValue(IsFlippedProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsFlipped.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFlippedProperty =
            DependencyProperty.Register(nameof(IsFlipped), typeof(bool), typeof(FlipPanel),
                new FrameworkPropertyMetadata(false, OnIsFlippedChanged));

        private static void OnIsFlippedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FlipPanel)d).ChangeVisualState(true);
        }

        #endregion

        #region HorizontalFlipButtonAlignment

        public HorizontalAlignment HorizontalFlipButtonAlignment
        {
            get => (HorizontalAlignment)GetValue(HorizontalFlipButtonAlignmentProperty);
            set => SetValue(HorizontalFlipButtonAlignmentProperty, value);
        }

        // Using a DependencyProperty as the backing store for HorizontalFlipButtonAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalFlipButtonAlignmentProperty =
            DependencyProperty.Register(nameof(HorizontalFlipButtonAlignment), typeof(HorizontalAlignment), typeof(FlipPanel),
           null);

        #endregion

        #region VerticalFlipButtonAlignment

        public VerticalAlignment VerticalFlipButtonAlignment
        {
            get => (VerticalAlignment)GetValue(VerticalFlipButtonAlignmentProperty);
            set => SetValue(VerticalFlipButtonAlignmentProperty, value);
        }

        // Using a DependencyProperty as the backing store for VerticalFlipButtonAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalFlipButtonAlignmentProperty =
            DependencyProperty.Register(nameof(VerticalFlipButtonAlignment), typeof(VerticalAlignment), typeof(FlipPanel),
            null);

        #endregion

        #region NavigationAreaBrush

        public Brush NavigationAreaBackgroundBrush
        {
            get => (Brush)GetValue(NavigationAreaBackgroundBrushProperty);
            set => SetValue(NavigationAreaBackgroundBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NavigationAreaBackgroundBrushProperty =
            DependencyProperty.Register(nameof(NavigationAreaBackgroundBrush), typeof(Brush), typeof(FlipPanel),
            null);

        #endregion

        #region FlipButtonMouseOverForeground

        public Brush FlipButtonMouseOverForeground
        {
            get => (Brush)GetValue(FlipButtonMouseOverForegroundProperty);
            set => SetValue(FlipButtonMouseOverForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for FlipButtonMouseOverForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FlipButtonMouseOverForegroundProperty =
            DependencyProperty.Register(nameof(FlipButtonMouseOverForeground), typeof(Brush), typeof(FlipPanel),
            new FrameworkPropertyMetadata(ForegroundProperty.DefaultMetadata.DefaultValue));

        #endregion

        #region Header

        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(nameof(Header), typeof(object), typeof(FlipPanel), null);

        #endregion

        #region CornerRadius

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for ConnerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(FlipPanel),
            new FrameworkPropertyMetadata(new CornerRadius(0)));

        #endregion

        #endregion

        private void ChangeVisualState(bool useTransition)
        {
            VisualStateManager.GoToState(this, IsFlipped ? "Flipped" : "Normal", useTransition);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ToggleButton tbBtn = (ToggleButton)GetTemplateChild("FlipButton");
            if (tbBtn != null)
            {
                tbBtn.Click += (s, e) => { IsFlipped = !IsFlipped; };

                tbBtn.MouseEnter += (s, e) => tbBtn.Foreground = FlipButtonMouseOverForeground;

                tbBtn.MouseLeave += (s, e) => tbBtn.Foreground = Foreground;
            }

            ChangeVisualState(false);
        }
    }
}
