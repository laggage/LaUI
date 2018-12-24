namespace LaUI.Controls
{
    using System.Windows;
    using System.Windows.Controls;

    public class AccordionItem:ListBoxItem
    {
        #region DependencyProperty

        #region Header

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(nameof(Header), typeof(string), 
                typeof(AccordionItem), new PropertyMetadata(string.Empty));

        #endregion

        #endregion

        #region Constructor

        static AccordionItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AccordionItem),
                new FrameworkPropertyMetadata(typeof(AccordionItem)));
        }

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #endregion
    }
}
