using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace UWPTeamWork
{
    public class SecondHandleStateTrigger : StateTriggerBase
    {
        public int TimeFlag
        {
            get { return (int)GetValue(TimeFlagProperty); }
            set { SetValue(TimeFlagProperty, value); }
        }
        public static readonly DependencyProperty TimeFlagProperty =
            DependencyProperty.Register("TimeFlag", typeof(int), typeof(SecondHandleStateTrigger), new PropertyMetadata(1));


        private static readonly DependencyProperty SecondHandleProperty = DependencyProperty.RegisterAttached("SecondHandle", typeof(int), typeof(SecondHandleStateTrigger),
            new PropertyMetadata(1, new PropertyChangedCallback(TimeChanged)));
        public int SecondHandle
        {
            get { return (int)GetValue(SecondHandleProperty); }
            set { SetValue(SecondHandleProperty, value); }
        }
        private static void TimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SecondHandleStateTrigger dat = (SecondHandleStateTrigger)d;
            //Debug.WriteLine(e.NewValue);
            dat.SetActive((int)e.NewValue % dat.TimeFlag == 0);
        }
    }
}
