using System;
using System.Data.Linq;
using System.Windows.Data;
using System.Windows.Media;
using GestioneOrdini.Cl;


namespace GestioneOrdini.Gui.Converters
{
    class RigaToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is RigaOrdine)
            {
                RigaOrdine dc = value as RigaOrdine;

                if (dc.IsRitirato)
                {
                    return new SolidColorBrush(Colors.Green);
                }
                else if (dc.IsAvvisato)
                {
                    return new SolidColorBrush(Colors.Yellow);
                }
                else if (dc.DataArrivoPezzo != null)
                {
                    return new SolidColorBrush(Colors.Red);
                }
            }

            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
