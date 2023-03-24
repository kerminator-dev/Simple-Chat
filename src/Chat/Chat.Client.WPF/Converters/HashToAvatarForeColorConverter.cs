using Chat.Client.WPF.Extensions;
using Chat.Client.WPF.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Chat.Client.WPF.Converters
{
    internal class HashToAvatarForeColorConverter : IValueConverter
    {
        public object Convert(object hash, Type targetType, object parameter, CultureInfo culture)
        {
            int index = (int)hash % AvatarColorsStore.Count;

            return new SolidColorBrush(AvatarColorsStore.GetForeColor(index));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
