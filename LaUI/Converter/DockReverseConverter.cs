using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace LaUI.Converter
{
    class DockPosReverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value?.GetType() != typeof(Dock))
                throw new ArgumentException("DockReverseConverter 无法转换除 class Dock 的其他class");

            return DockPosReverse((Dock)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(Dock))
                throw new ArgumentException("DockReverseConverter 无法转换除 class Dock 的其他class");

            return DockPosReverse((Dock) value);
        }

        private Dock DockPosReverse(Dock dock)
        {
            Dock result;
            switch (dock)
            {
                case Dock.Left:
                    result = Dock.Right;
                    break;
                case Dock.Right:
                    result = Dock.Left;
                    break;
                case Dock.Bottom:
                    result = Dock.Top;
                    break;
                case Dock.Top:
                    result = Dock.Bottom;
                    break;
                default:
                    throw new ArgumentException("无法对参数进行转换");
            }

            return result;
        }
    }
}
