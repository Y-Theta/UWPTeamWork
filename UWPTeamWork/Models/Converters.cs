using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    //Ang to Min 
    public class MinAngConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter != null)
                return String.Format("{0:D2}:{1:D2}:{2:D2}", (int)value / 1500, (int)value % 1500 / 25, (int)value % 1500 % 25 * 4);
            else
                return String.Format("{0:D2}:{1:D2}", (int)value / 60, (int)value % 60);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (bool)value;
        }
    }

    //Simple Ang to Min 
    public class MinAngConverterSim : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter != null)
            {
                return String.Format("{0:D2}:{1:D2}:{2:D2}", (int)value / 1500, (int)value % 1500 / 25, (int)value % 1500 % 25 * 4);
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

    //Ang to Hour
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

    //格式化时间
    public class TimeFormatdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
                return String.Format("{0:D2}:{1:D2}.{2:D2}", ((TimeSpan)value).Minutes, ((TimeSpan)value).Seconds, ((TimeSpan)value).Milliseconds);
            else
                return " ";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    
}
