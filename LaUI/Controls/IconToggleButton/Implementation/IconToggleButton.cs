using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace LaUI.Control
{
    public class IconToggleButton:ToggleButton
    {
        #region Constructor

        static IconToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconToggleButton), 
                new FrameworkPropertyMetadata(typeof(IconToggleButton)));
        }

        #endregion

        #region Property

        #region DependencyProperty

        #region IconTemplate

        public DataTemplate IconTemplate
        {
            get => (DataTemplate)GetValue(IconTemplateProperty);
            set => SetValue(IconTemplateProperty, value);
        }

        // Using a DependencyProperty as the backing store for IconTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconTemplateProperty =
            DependencyProperty.Register(nameof(IconTemplate), typeof(DataTemplate),
                typeof(IconToggleButton), new PropertyMetadata());

        #endregion

        #region Icon

        public object Icon
        {
            get => GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(object), typeof(IconToggleButton),
                new FrameworkPropertyMetadata(null));

        #endregion

        #region IconVisibility

        public Visibility IconVisibility
        {
            get => (Visibility)GetValue(IconVisibilityProperty);
            set => SetValue(IconVisibilityProperty, value);
        }

        // Using a DependencyProperty as the backing store for IconVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconVisibilityProperty =
            DependencyProperty.Register(nameof(IconVisibility), typeof(Visibility), typeof(IconToggleButton), 
                new PropertyMetadata(Visibility.Visible));

        #endregion

        #region CornerRadius

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(IconToggleButton), 
                new PropertyMetadata(new CornerRadius(0)));

        #endregion

        #region IconPosition

        public Dock IconPosition
        {
            get => (Dock)GetValue(IconPositionProperty);
            set => SetValue(IconPositionProperty, value);
        }

        // Using a DependencyProperty as the backing store for IconPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconPositionProperty =
            DependencyProperty.Register(nameof(IconPosition), typeof(Dock), typeof(IconToggleButton), 
                new FrameworkPropertyMetadata(Dock.Left));

        #endregion

        #endregion

        #endregion

        #region Method

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #endregion

        #endregion
    }
}
