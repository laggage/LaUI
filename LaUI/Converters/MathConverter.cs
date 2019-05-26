using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace LaUI.Converters
{
    public enum MathOperation
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public sealed class MathConverter:IValueConverter,IMultiValueConverter
    {
        public MathOperation MathOperation { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DoConverter(value, parameter, MathOperation);
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Length < 2 ? Binding.DoNothing : DoConverter(values[0], values[1], MathOperation);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return targetTypes.Select(t => Binding.DoNothing).ToArray();
        }

        public object DoConverter(object firstValue, object secondValue,MathOperation operation)
        {
            if (firstValue == null ||
                secondValue == null ||
                firstValue == DependencyProperty.UnsetValue ||
                secondValue == DependencyProperty.UnsetValue)
            {
                return Binding.DoNothing;
            }

            double first = (firstValue as double?).GetValueOrDefault(System.Convert.ToDouble(firstValue));
            double second = (secondValue as double?).GetValueOrDefault(System.Convert.ToDouble(secondValue));
            switch (operation)
            {
                case MathOperation.Add:
                    return first + second;
                case MathOperation.Subtract:
                    return first - second;
                case MathOperation.Multiply:
                    return first * second;
                case MathOperation.Divide:
                    return first / second;
                default:
                    return Binding.DoNothing;
            }
        }
    }

    [MarkupExtensionReturnType(typeof(MathAddConverter))]
    public sealed class MathAddConverter:MarkUpMultiConverter
    {
        private MathAddConverter _instance;
        private MathConverter _mathConverter = null;
        private MathConverter MathConverter =>
            _mathConverter = _mathConverter ?? new MathConverter()
            {
                MathOperation = MathOperation.Add
            };

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return _mathConverter.Convert(
                value, targetType, parameter, culture);
        }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return MathConverter.Convert(values, targetType, parameter, culture);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return MathConverter.ConvertBack(value, targetType, parameter, culture);
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return MathConverter.ConvertBack(value, targetTypes, parameter, culture);
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new MathAddConverter());
        }
    }

    [MarkupExtensionReturnType(typeof(MathMultiPlyConverter))]
    public sealed class MathMultiPlyConverter : MarkUpMultiConverter
    {
        private MathMultiPlyConverter _instance;
        private MathConverter _mathConverter = null;
        private MathConverter MathConverter =>
            _mathConverter = _mathConverter ?? new MathConverter()
            {
                MathOperation = MathOperation.Multiply
            };

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return _mathConverter.Convert(
                value, targetType, parameter, culture);
        }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return MathConverter.Convert(values, targetType, parameter, culture);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return MathConverter.ConvertBack(value, targetType, parameter, culture);
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return MathConverter.ConvertBack(value, targetTypes, parameter, culture);
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new MathMultiPlyConverter());
        }
    }

    [MarkupExtensionReturnType(typeof(MathSubtractConverter))]
    public sealed class MathSubtractConverter:MarkUpMultiConverter
    {
        private MathSubtractConverter _instance;

        private MathConverter _mathConverter;
        private MathConverter MathConverter =>
            _mathConverter ?? (_mathConverter = new MathConverter()
            {
                MathOperation = MathOperation.Subtract
            });

        static MathSubtractConverter()
        {

        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return MathConverter.Convert(
                value, targetType, parameter, culture);
        }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return MathConverter.Convert(values, targetType, parameter, culture);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return MathConverter.ConvertBack(value, targetType, parameter, culture);
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return MathConverter.ConvertBack(value, targetTypes, parameter, culture);
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new MathSubtractConverter());
        }
    }

    public sealed class MathDivideConverter:MarkUpMultiConverter
    {
        private MathDivideConverter _instance;
        private MathConverter _mathConverter;
        private MathConverter MathConverter =>
            _mathConverter ?? (_mathConverter = new MathConverter()
            {
                MathOperation = MathOperation.Divide
            });

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return _mathConverter.Convert(
                value, targetType, parameter, culture);
        }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return MathConverter.Convert(values, targetType, parameter, culture);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return MathConverter.ConvertBack(value, targetType, parameter, culture);
        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return MathConverter.ConvertBack(value, targetTypes, parameter, culture);
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new MathDivideConverter());
        }
    }
}
