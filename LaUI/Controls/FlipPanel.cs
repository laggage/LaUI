using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace LaUI.Controls
{
    [TemplateVisualState(GroupName = "ViewStates", Name = "Flipped")]
    [TemplateVisualState(GroupName = "ViewStates", Name = "Normal")]
    [TemplatePart(Name = FlipButtonName, Type = typeof(ToggleButton))]
    [TemplatePart(Name = FlipButtonAlternateName, Type = typeof(ToggleButton))]
    public class FlipPanel:Control
    {
        private const string FlipButtonName = "PART_FlipButton";
        private const string FlipButtonAlternateName = "PART_FlipButtonAlternate";

        public object BackContent
        {
            get => (object)GetValue(BackContentProperty);
            set => SetValue(BackContentProperty, value);
        }
        // Using a DependencyProperty as the backing store for BackContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackContentProperty =
            DependencyProperty.Register(nameof(BackContent), typeof(object), typeof(FlipPanel),
                new FrameworkPropertyMetadata(default(object)));

        public object FrontContent
        {
            get => (object)GetValue(FrontContentProperty);
            set => SetValue(FrontContentProperty, value);
        }
        // Using a DependencyProperty as the backing store for FrontContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FrontContentProperty =
            DependencyProperty.Register(nameof(FrontContent), typeof(object), typeof(FlipPanel),
            new FrameworkPropertyMetadata(default(object)));

        public bool IsFlipped
        {
            get => (bool)GetValue(IsFlippedProperty);
            set => SetValue(IsFlippedProperty, value);
        }
        // Using a DependencyProperty as the backing store for IsFlipped.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFlippedProperty =
            DependencyProperty.Register(nameof(IsFlipped), typeof(bool), typeof(FlipPanel),
            new FrameworkPropertyMetadata(false, (dp, arg) =>
                {
                    dp = dp ?? throw new ArgumentNullException(nameof(dp));
                    FlipPanel flipPanel = (dp as FlipPanel) ?? throw new ArgumentOutOfRangeException();
                    flipPanel.ChangeVisualState();
                }));

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(FlipPanel),
            new FrameworkPropertyMetadata(default(CornerRadius)));

        /// <summary>
        /// The content need to be displayed aside the flip button which may be an icon button
        /// </summary>
        public object FlipHeader
        {
            get => GetValue(FlipHeaderProperty);
            set => SetValue(FlipHeaderProperty, value);
        }
        public static readonly DependencyProperty FlipHeaderProperty =
            DependencyProperty.Register(
                nameof(FlipHeader), typeof(object), typeof(FlipPanel), 
                new FrameworkPropertyMetadata(default(object)));

        /// <summary>
        /// The content which will be displayed upon the top of the card
        /// </summary>
        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(
                nameof(Header), typeof(object), typeof(FlipPanel),
                new FrameworkPropertyMetadata(default(object)));


        public HorizontalAlignment HorizontalHeaderAlignment
        {
            get => (HorizontalAlignment)GetValue(HorizontalHeaderAlignmentProperty);
            set => SetValue(HorizontalHeaderAlignmentProperty, value);
        }
        // Using a DependencyProperty as the backing store for HorizontalHeaderAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalHeaderAlignmentProperty =
            DependencyProperty.Register(
                nameof(HorizontalHeaderAlignment), typeof(HorizontalAlignment), typeof(FlipPanel),
                new FrameworkPropertyMetadata(HorizontalAlignment.Center));

        public VerticalAlignment VerticalHeaderAlignment
        {
            get => (VerticalAlignment)GetValue(VerticalHeaderAlignmentProperty);
            set => SetValue(VerticalHeaderAlignmentProperty, value);
        }
        // Using a DependencyProperty as the backing store for VerticalHeaderAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalHeaderAlignmentProperty =
            DependencyProperty.Register(nameof(VerticalHeaderAlignment), typeof(VerticalAlignment), typeof(FlipPanel),
            new FrameworkPropertyMetadata(VerticalAlignment.Center));

        public HorizontalAlignment HorizontalFlipHeaderAlignment
        {
            get => (HorizontalAlignment)GetValue(HorizontalFlipHeaderAlignmentProperty);
            set => SetValue(HorizontalFlipHeaderAlignmentProperty, value);
        }
        // Using a DependencyProperty as the backing store for HorizontalFlipHeaderAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalFlipHeaderAlignmentProperty =
            DependencyProperty.Register(nameof(HorizontalFlipHeaderAlignment), typeof(HorizontalAlignment), typeof(FlipPanel),
            new FrameworkPropertyMetadata(HorizontalAlignment.Center));

        public VerticalAlignment VerticalFlipHeaderAlignment
        {
            get => (VerticalAlignment)GetValue(VerticalFlipHeaderAlignmentProperty);
            set => SetValue(VerticalFlipHeaderAlignmentProperty, value);
        }
        // Using a DependencyProperty as the backing store for VerticalFlipHeaderAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalFlipHeaderAlignmentProperty =
            DependencyProperty.Register(nameof(VerticalFlipHeaderAlignment), typeof(VerticalAlignment), typeof(FlipPanel),
            new FrameworkPropertyMetadata(VerticalAlignment.Center));

        static FlipPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(FlipPanel),
                new FrameworkPropertyMetadata(typeof(FlipPanel)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (GetTemplateChild(FlipButtonName) is ToggleButton btn)
                btn.Click += flipButton_Click;

            if (GetTemplateChild(FlipButtonAlternateName) is ToggleButton btnAlternate)
                btnAlternate.Click += flipButton_Click;
        }

        private void flipButton_Click(object sender, RoutedEventArgs e)
        {
            IsFlipped = !IsFlipped;
        }

        private void ChangeVisualState(bool useTransitions = true)
        {
            VisualStateManager.GoToState(this, IsFlipped ? "Flipped" : "Normal", useTransitions);
        }
    }
}
