using Chat.Client.WPF.Extensions;
using Chat.Client.WPF.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Chat.Client.WPF.Converters
{
    [ValueConversion(typeof(string), typeof(SolidColorBrush))]
    internal class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string)
                return new SolidColorBrush(Color.FromRgb(0,0,0));

            return null;
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
        
    }
}
