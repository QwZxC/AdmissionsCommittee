using System;
using System.Globalization;
using System.Windows.Data;

namespace AdmissionsCommittee.Converters
{
    public class PrefixValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            string s = value.ToString();
            int prefixLength;
            if (!int.TryParse(parameter.ToString(), out prefixLength) || s.Length <= prefixLength)
            {
                return s;
            }

            return $"{s.Substring(0, prefixLength)}.";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
