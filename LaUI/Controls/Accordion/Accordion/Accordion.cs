namespace LaUI.Control
{
    using System.Windows;
    using System.Windows.Controls;

    public class Accordion:ListBox
    {
        #region Constructor

        static Accordion()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Accordion),
                new FrameworkPropertyMetadata(typeof(Accordion)));
        }

        #endregion

        #region Property

        #region DependencyProperty


        #endregion

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #endregion
    }
}
