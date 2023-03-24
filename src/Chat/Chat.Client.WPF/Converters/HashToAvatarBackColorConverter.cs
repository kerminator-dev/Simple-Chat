using Chat.Client.WPF.Services;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Chat.Client.WPF.Converters
{
    [ValueConversion(typeof(int), typeof(SolidColorBrush))]
    internal class HashToAvatarBackColorConverter : IValueConverter
    {
        public object Convert(object hash, Type targetType, object parameter, CultureInfo culture)
        {
            int index = (int)hash % AvatarColorsStore.Count;

            return new SolidColorBrush(AvatarColorsStore.GetBackColor(index));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }
}
