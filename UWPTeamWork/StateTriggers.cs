using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace UWPTeamWork
{
    public class TickTextTrigger : StateTriggerBase
    {
        public int ActiveAngle
        {
            get { return (int)GetValue(ActiveAngleProperty); }
            set { SetValue(ActiveAngleProperty, value); }
        }
        public static readonly DependencyProperty ActiveAngleProperty =
            DependencyProperty.Register("ActiveAngle", typeof(int), typeof(TickTextTrigger), new PropertyMetadata(0));


        private static readonly DependencyProperty AngelProperty = DependencyProperty.RegisterAttached("Angel", typeof(double), typeof(TickTextTrigger),
        new PropertyMetadata(1, new PropertyChangedCallback(TimeChanged)));
        public double Angel
        {
            get { return (double)GetValue(AngelProperty); }
            set { SetValue(AngelProperty, value); }
        }
        private static void TimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TickTextTrigger dat = (TickTextTrigger)d;
            dat.SetActive(Math.Abs((double)e.NewValue - dat.ActiveAngle) < 6);
        }
    }
}
