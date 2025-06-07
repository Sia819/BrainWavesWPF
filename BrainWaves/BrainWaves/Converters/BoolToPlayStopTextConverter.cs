using System;
using System.Globalization;
using System.Windows.Data;

namespace BrainWaves.Converters
{
    public class BoolToPlayStopTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isPlaying)
            {
                return isPlaying ? "Stop" : "Play";
            }
            return "Play";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}