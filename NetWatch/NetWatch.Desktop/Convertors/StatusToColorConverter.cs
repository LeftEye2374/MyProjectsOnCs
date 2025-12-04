using NetWatch.Model.Enums;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace NetWatch.Desktop.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is NetStatus status)
            {
                return status switch
                {
                    NetStatus.Online => Brushes.Green,
                    NetStatus.Offline => Brushes.Red,
                    NetStatus.Unknown => Brushes.Gray,
                    _ => Brushes.LightGray
                };
            }

            if (value is AlertLevel level)
            {
                return level switch
                {
                    AlertLevel.Info => Brushes.Blue,
                    AlertLevel.Warning => Brushes.Orange,
                    AlertLevel.Critical => Brushes.Red,
                    _ => Brushes.Gray
                };
            }

            return Brushes.LightGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}