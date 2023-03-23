using Chat.Core.Enums;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Chat.Client.WPF.Converters
{
    internal class OnlineStatusToColorConverter : IValueConverter
    {
        private static SolidColorBrush OnlineStatusColor = new SolidColorBrush(Color.FromRgb(119, 140, 235));

        private static SolidColorBrush OfflineStatusColor = new SolidColorBrush(Color.FromRgb(234, 134, 134));
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not OnlineStatus)
                return null;

            switch ((OnlineStatus)value)
            {
                case OnlineStatus.Online:
                    return OnlineStatusColor;
                default:
                    return OfflineStatusColor; 
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
