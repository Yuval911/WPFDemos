using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFDemos
{
    public class SliderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int number = System.Convert.ToInt32(value);

            if (number >= 100 && number <= 200) // in case of a width slider
            {
                if (number < 125)
                    return "Small";
                if (number < 150)
                    return "Meduim";
                if (number < 175)
                    return "Large";
                return "Extra Large";
            }

            if (number >= 30 && number <= 60) // in case of a height slider
            {
                if (number < 30 + 30 * 0.25)
                    return "Small";
                if (number < 30 + 30 * 0.50)
                    return "Meduim";
                if (number < 30 + 30 * 0.75)
                    return "Large";
                return "Extra Large";
            }

            return "Invalid value";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
