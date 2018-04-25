using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using static UWPTeamWork.SlideClock;

namespace UWPTeamWork
{
    public class MinAngConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter != null)
                return String.Format("{0:D2} : {1:D2} : {2:D2}", (int)value / 1500, (int)value % 1500 / 25, (int)value % 1500 % 25);
            else
                return String.Format("{0:D2} : {1:D2}", (int)value / 60, (int)value % 60);

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (bool)value;
        }
    }

    public class MinAngConverterSim : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter != null)
            {
                return String.Format("{0:D2} : {1:D2} : {2:D2}", (int)value / 1500, (int)value % 1500 / 25, (int)value % 1500 % 25);
            }
            else
            {
                if ((int)value / 60 == 0)
                    return String.Format(".{0:D}", (int)value % 60);
                else
                    return String.Format("{0:D}", (int)value / 60);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (bool)value;
        }
    }

    public class HourAngConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (bool)value;
        }
    }

    public class MutexVisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch((Visibility)value)
            {
                case Visibility.Collapsed:
                    return Visibility.Visible;
                default:return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
