using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace LaUI.Controls.Helper
{
    public class TextBoxHelper
    {
        public static readonly DependencyProperty WaterMarkProperty =
            DependencyProperty.RegisterAttached(
                "WaterMark", typeof(string),
                typeof(TextBoxHelper), new FrameworkPropertyMetadata(string.Empty));

        public static readonly DependencyProperty WaterMarkTextAlignmentProperty =
            DependencyProperty.RegisterAttached(
                "WaterMarkTextAlignment", typeof(TextAlignment),
                typeof(TextBoxHelper), new FrameworkPropertyMetadata(TextAlignment.Left));

        public static readonly DependencyProperty WaterMarkTextWrappingProperty =
            DependencyProperty.RegisterAttached(
                "WaterMarkTextWrapping", typeof(TextWrapping),
                typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(TextWrapping.NoWrap));

        public static readonly DependencyProperty WaterMarkTextTrimmingProperty =
            DependencyProperty.RegisterAttached(
                "WaterMarkTextTrimming", typeof(TextTrimming),
                typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(TextTrimming.None));

        public static readonly DependencyProperty UseFloatingWaterMarkProperty =
            DependencyProperty.RegisterAttached(
                "UseFloatingWaterMark", typeof(bool),
                typeof(TextBoxHelper), new FrameworkPropertyMetadata(false, OnUseFloatingWatermarkChanged));

        public static readonly DependencyProperty HasTextProperty =
            DependencyProperty.RegisterAttached(
                "HasText", typeof(bool),
                typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(
                    false, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty HasClearTextButtonProperty =
            DependencyProperty.RegisterAttached(
                "HasClearTextButton", typeof(bool),
                typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty ClearTextButtonAlignmentProperty =
            DependencyProperty.RegisterAttached(
                "ClearTextButtonAlignment", typeof(ButtonAlignment),
                typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(
                    ButtonAlignment.Right, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange |
                                           FrameworkPropertyMetadataOptions.AffectsRender));


        public static readonly DependencyProperty TextButtonWidthProperty =
            DependencyProperty.RegisterAttached(
                "TextButtonWidth",
                typeof(double), typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(
                    default(double),
                    FrameworkPropertyMetadataOptions.AffectsArrange |
                    FrameworkPropertyMetadataOptions.AffectsParentMeasure));

        public static readonly DependencyProperty TextButtonFamilyProperty =
            DependencyProperty.RegisterAttached(
                "TextButtonFamily", typeof(FontFamily),
                typeof(TextBoxHelper),
                new FrameworkPropertyMetadata((new FontFamilyConverter()).ConvertFromString("Marlett")));


        public static readonly DependencyProperty TextButtonContentProperty =
            DependencyProperty.RegisterAttached(
                "TextButtonContent", typeof(object), typeof(TextBoxHelper),
                new FrameworkPropertyMetadata("r"));

        public static readonly DependencyProperty TextButtonContentTemplateProperty =
            DependencyProperty.RegisterAttached(
                "TextButtonContentTemplate", typeof(DataTemplate), typeof(TextBoxHelper),
                new FrameworkPropertyMetadata((DataTemplate)null));

        public static readonly DependencyProperty TextButtonFontSizeProperty =
            DependencyProperty.RegisterAttached(
                "TextButtonFontSize", typeof(double), typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(SystemFonts.IconFontSize));

        private static void OnUseFloatingWatermarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is TextBox textBox)) return;
            textBox.TextChanged -= OnTextChanged;
            textBox.TextChanged += OnTextChanged;
        }

        private static void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
                textBox.SetValue(
                    HasTextProperty, textBox.Text.Length > 0);
        }

        public static string GetWaterMark(DependencyObject obj)
        {
            return (string)obj.GetValue(WaterMarkProperty);
        }

        public static void SetWaterMark(DependencyObject obj, string value)
        {
            obj.SetValue(WaterMarkProperty, value);
        }

        public static TextAlignment GetWaterMarkTextAlignment(DependencyObject obj)
        {
            return (TextAlignment)obj.GetValue(WaterMarkTextAlignmentProperty);
        }

        public static void SetWaterMarkTextAlignment(DependencyObject obj, TextAlignment value)
        {
            obj.SetValue(WaterMarkTextAlignmentProperty, value);
        }

        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
        public static TextWrapping GetWaterMarkTextWrapping(DependencyObject obj)
        {
            return (TextWrapping)obj.GetValue(WaterMarkTextWrappingProperty);
        }

        public static void SetWaterMarkTextWrapping(DependencyObject obj, TextWrapping value)
        {
            obj.SetValue(WaterMarkTextWrappingProperty, value);
        }

        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
        public static TextTrimming GetWaterMarkTextTrimming(DependencyObject obj)
        {
            return (TextTrimming)obj.GetValue(WaterMarkTextTrimmingProperty);
        }

        public static void SetWaterMarkTextTrimming(DependencyObject obj, TextTrimming value)
        {
            obj.SetValue(WaterMarkTextTrimmingProperty, value);
        }

        [AttachedPropertyBrowsableForType(typeof(TextBoxBase))]
        public static bool GetUseFloatingWaterMark(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseFloatingWaterMarkProperty);
        }

        public static void SetUseFloatingWaterMark(DependencyObject obj, bool value)
        {
            obj.SetValue(UseFloatingWaterMarkProperty, value);
        }

        public static bool GetHasText(DependencyObject obj)
        {
            return (bool)obj.GetValue(HasTextProperty);
        }


        public static bool GetHasClearTextButton(DependencyObject obj)
        {
            return (bool)obj.GetValue(HasClearTextButtonProperty);
        }

        public static void SetHasClearTextButton(DependencyObject obj, bool value)
        {
            obj.SetValue(HasClearTextButtonProperty, value);
        }

        public static ButtonAlignment GetClearTextButtonAlignment(DependencyObject obj)
        {
            return (ButtonAlignment)obj.GetValue(ClearTextButtonAlignmentProperty);
        }

        public static void SetClearTextButtonAlignment(DependencyObject obj, ButtonAlignment value)
        {
            obj.SetValue(ClearTextButtonAlignmentProperty, value);
        }

        public static double GetTextButtonWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(TextButtonWidthProperty);
        }

        public static void SetTextButtonWidth(DependencyObject obj, double value)
        {
            obj.SetValue(TextButtonWidthProperty, value);
        }

        public static FontFamily GetTextButtonFamily(DependencyObject obj)
        {
            return (FontFamily)obj.GetValue(TextButtonFamilyProperty);
        }

        public static void SetTextButtonFamily(DependencyObject obj, FontFamily value)
        {
            obj.SetValue(TextButtonFamilyProperty, value);
        }

        
        public static object GetTextButtonContent(DependencyObject obj)
        {
            return (object)obj.GetValue(TextButtonContentProperty);
        }

        public static void SetTextButtonContent(DependencyObject obj, object value)
        {
            obj.SetValue(TextButtonContentProperty, value);
        }

        public static DataTemplate GetTextButtonContentTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(TextButtonContentTemplateProperty);
        }

        public static void SetTextButtonContentTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(TextButtonContentTemplateProperty, value);
        }

        public static double GetTextButtonFontSize(DependencyObject obj)
        {
            return (double)obj.GetValue(TextButtonFontSizeProperty);
        }

        public static void SetTextButtonFontSize(DependencyObject obj, double value)
        {
            obj.SetValue(TextButtonFontSizeProperty, value);
        }


        public static bool GetIsTextClearButtonBehaviorEnable(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsTextClearButtonBehaviorEnableProperty);
        }

        public static void SetIsTextClearButtonBehaviorEnable(DependencyObject obj, bool value)
        {
            obj.SetValue(IsTextClearButtonBehaviorEnableProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsTextClearButtonBehaviorEnable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTextClearButtonBehaviorEnableProperty =
            DependencyProperty.RegisterAttached(
                "IsTextClearButtonBehaviorEnable", typeof(bool), typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(false, (s, e) =>
                {
                    if (s is Button btn)
                    {
                        btn.Click += ButtonClicked;
                    }
                }));

        private static void ButtonClicked(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                DependencyObject i = VisualTreeHelper.GetParent(btn);
                while (i != null&&!((i = VisualTreeHelper.GetParent(i)) is TextBox)) ;
                if (i == null) return;
                (i as TextBox).Clear();
            }
        }
    }
}
