using System;
using System.Windows.Data;


namespace GestioneOrdini.Gui.Converters
{
    public class DatetimeToDateConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is DateTime)
            {
                DateTime d = (DateTime)value;

                return d.ToString("dd-MM-yyyy");
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
