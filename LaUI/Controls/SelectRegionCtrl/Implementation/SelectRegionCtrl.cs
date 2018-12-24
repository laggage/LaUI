namespace LaUI.Control
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class SelectRegionCtrl : Thumb
    {
        #region Constructor

        static SelectRegionCtrl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectRegionCtrl),
                new FrameworkPropertyMetadata(typeof(SelectRegionCtrl)));
        }

        public SelectRegionCtrl()
        {
            Loaded += SelectRegionCtrl_Loaded;
            DragDelta += SelectRegionCtrl_DragDelta;
            SizeChanged += (sender, e) => { SelectedRegion = new Rect(SelectedRegion.TopLeft, e.NewSize); };
        }

        #endregion

        #region DependencyProperty

        #region SelectedRegion

        public Rect SelectedRegion
        {
            get => (Rect) GetValue(SelectedRegionProperty);
            set => SetValue(SelectedRegionProperty, value);
        }

        // Using a DependencyProperty as the backing store for Region.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedRegionProperty =
            DependencyProperty.Register(nameof(SelectedRegion), typeof(Rect), typeof(SelectRegionCtrl),
                new FrameworkPropertyMetadata(OnSelectedRegionChanged));
       
        #endregion

        #region DragRegion

        public Rect DragRegion
        {
            get => (Rect)GetValue(DragRegionProperty);
            set => SetValue(DragRegionProperty, value);
        }

        // Using a DependencyProperty as the backing store for DragRegion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragRegionProperty =
            DependencyProperty.Register(nameof(DragRegion), typeof(Rect),
                typeof(SelectRegionCtrl), new PropertyMetadata(new Rect(0, 0, 0, 0)));

        #endregion

        #region SelectTemplate

        public DataTemplate SelectTemplate
        {
            get => (DataTemplate)GetValue(SelectTemplateProperty);
            set => SetValue(SelectTemplateProperty, value);
        }

        // Using a DependencyProperty as the backing store for SelectTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectTemplateProperty =
            DependencyProperty.Register(nameof(SelectTemplate), typeof(DataTemplate), typeof(SelectRegionCtrl));

        #endregion

        #region Content

        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(nameof(Content), typeof(object), typeof(SelectRegionCtrl));

        #endregion

        #endregion

        #region Property

        private Canvas ContainedBy
        {
            get
            {
                var parent = VisualTreeHelper.GetParent(this);
                if (parent != null && !(parent.GetType() == typeof(Canvas)))
                    throw new System.Exception("SelectRegionCtrl's parent must be canvas");
                return (Canvas)parent;
            }
        }

        #endregion

        #region Method

        private void SetPosition(double left,double top)
        {
            Canvas.SetLeft(this,left);
            Canvas.SetTop(this, top);
        }

        #endregion

        #region EventHandler

        private void SelectRegionCtrl_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double left = Canvas.GetLeft(this) + e.HorizontalChange,
                top = Canvas.GetTop(this) + e.VerticalChange;
            //判断新的位置是否在设定的区域中
            if (DragRegion.Width > 0 || DragRegion.Height > 0)
            {
                if (left < DragRegion.Left || left + SelectedRegion.Width > DragRegion.Right)
                    left = Canvas.GetLeft(this);
                if (top < DragRegion.Top || top + SelectedRegion.Height > DragRegion.Bottom)
                    top = Canvas.GetTop(this);
            }

            SelectedRegion = new Rect(left, top, Width, Height);
            e.Handled = false;
        }

        private void SelectRegionCtrl_Loaded(object sender, RoutedEventArgs e)
        {
            if (ContainedBy != null)
                ContainedBy.SizeChanged += ContainedBy_SizeChanged;
            SetPosition(SelectedRegion.X, SelectedRegion.Y);
        }

        private void ContainedBy_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (SelectedRegion.Right > ContainedBy.ActualWidth)
                Canvas.SetLeft(this, ContainedBy.ActualWidth - SelectedRegion.Width);
            if (SelectedRegion.Bottom > ContainedBy.ActualHeight)
                Canvas.SetTop(this, ContainedBy.ActualHeight - SelectedRegion.Height);
            e.Handled = false;
        }

        private static void OnSelectedRegionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SelectRegionCtrl s = (SelectRegionCtrl)d;
            Rect r = (Rect)e.NewValue;
            s.SetPosition(r.X, r.Y);
        }

        #endregion
    }
}