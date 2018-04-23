﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace UWPTeamWork
{

    public class MinAngConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return String.Format("{0:D2} : {1:D2}", (int)value / 60, (int)value % 60);
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
}
