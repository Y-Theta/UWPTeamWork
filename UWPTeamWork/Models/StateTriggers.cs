using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using static UWPTeamWork.SlideClock;

namespace UWPTeamWork
{
    //刻度文字控制触发器
    public class TickTextTrigger : StateTriggerBase
    {
        public TimerMode Mode
        {
            get { return (TimerMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(TimerMode), typeof(TickTextTrigger), new PropertyMetadata(TimerMode.Timer));

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
            switch (dat.Mode)
            {
                case TimerMode.StopWatch:
                    dat.SetActive(Math.Abs((double)e.NewValue - dat.ActiveAngle) < 6);
                    break;
                default:
                    dat.SetActive(Math.Abs(360 + (double)e.NewValue - dat.ActiveAngle) < 6);
                    break;
            }
        }
    }

    public class BoolTrigger : StateTriggerBase
    {
        public bool ReferenceValue
        {
            get { return (bool)GetValue(ReferenceValueProperty); }
            set { SetValue(ReferenceValueProperty, value); }
        }
        public static readonly DependencyProperty ReferenceValueProperty =
            DependencyProperty.Register("ReferenceValue", typeof(bool), typeof(BoolTrigger), new PropertyMetadata(false));

        public bool BindValue
        {
            get { return (bool)GetValue(BindValueProperty); }
            set { SetValue(BindValueProperty, value); }
        }
        public static readonly DependencyProperty BindValueProperty =
            DependencyProperty.Register("BindValue", typeof(bool), typeof(BoolTrigger), new PropertyMetadata(false, new PropertyChangedCallback(OnBindValueChanged)));
        private static void OnBindValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BoolTrigger b = (BoolTrigger)d;
            b.SetActive((bool)e.NewValue.Equals(b.ReferenceValue));
        }
    }

    //窗口大小变化触发
    public class WindowSizeOverTrigger : StateTriggerBase
    {
        private bool flag = false;
        public double MinWindowHeight
        {
            get { return (double)GetValue(MinWindowHeightProperty); }
            set { SetValue(MinWindowHeightProperty, value); }
        }
        public static readonly DependencyProperty MinWindowHeightProperty =
            DependencyProperty.Register("MinWindowHeight", typeof(double), typeof(WindowSizeOverTrigger), new PropertyMetadata(0.0));

        public double MinWidowWidth
        {
            get { return (double)GetValue(MinWidowWidthProperty); }
            set { SetValue(MinWidowWidthProperty, value); }
        }
        public static readonly DependencyProperty MinWidowWidthProperty =
            DependencyProperty.Register("MinWidowWidth", typeof(double), typeof(WindowSizeOverTrigger), new PropertyMetadata(0.0));

        public WindowSizeOverTrigger()
        {
            CoreApplication.GetCurrentView().CoreWindow.SizeChanged += CoreWindow_SizeChanged;
        }

        private void CoreWindow_SizeChanged(CoreWindow sender, WindowSizeChangedEventArgs args)
        {
            if ((args.Size.Width > MinWidowWidth && args.Size.Height > MinWindowHeight) && !flag)
            {
                SetActive(true);
                flag = true;
            }
            if (args.Size.Width < MinWidowWidth || args.Size.Height < MinWindowHeight && flag)
            {
                flag = false;
                SetActive(false);
            }
        }
    }

    public class WindowSizeLessTrigger : StateTriggerBase
    {
        #region
        private bool flag = false;
        public double MinWindowHeight
        {
            get { return (double)GetValue(MinWindowHeightProperty); }
            set { SetValue(MinWindowHeightProperty, value); }
        }
        public static readonly DependencyProperty MinWindowHeightProperty =
            DependencyProperty.Register("MinWindowHeight", typeof(double), typeof(WindowSizeLessTrigger), new PropertyMetadata(0.0));

        public double MinWidowWidth
        {
            get { return (double)GetValue(MinWidowWidthProperty); }
            set { SetValue(MinWidowWidthProperty, value); }
        }
        public static readonly DependencyProperty MinWidowWidthProperty =
            DependencyProperty.Register("MinWidowWidth", typeof(double), typeof(WindowSizeLessTrigger), new PropertyMetadata(0.0));
        #endregion
        public WindowSizeLessTrigger()
        {
            CoreApplication.GetCurrentView().CoreWindow.SizeChanged += CoreWindow_SizeChanged;
        }

        private void CoreWindow_SizeChanged(CoreWindow sender, WindowSizeChangedEventArgs args)
        {
            if (!flag && (args.Size.Width < MinWidowWidth || args.Size.Height < MinWindowHeight))
            {
                SetActive(true);
                flag = true;
            }
            else if (flag && (args.Size.Width > MinWidowWidth && args.Size.Height > MinWindowHeight))
            {
                flag = false;
                SetActive(false);
            }
        }
    }

    //计时模式触发
    public class SlideClockModeTrigger : StateTriggerBase
    {
        public TimerMode Mode
        {
            get { return (TimerMode)GetValue(modeProperty); }
            set { SetValue(modeProperty, value); }
        }
        public static readonly DependencyProperty modeProperty =
            DependencyProperty.Register("Mode", typeof(TimerMode), typeof(SlideClockModeTrigger),
                new PropertyMetadata(TimerMode.Timer,new PropertyChangedCallback(OnModeChanged)));
        private static void OnModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SlideClockModeTrigger st = (SlideClockModeTrigger)d;
           // Debug.WriteLine((TimerMode)e.NewValue + "  " + st.Reference +"  " + ((TimerMode)e.NewValue).Equals(st.Reference));
            st.SetActive(((TimerMode)e.NewValue).Equals(st.Reference));
        }

        public TimerMode Reference
        {
            get { return (TimerMode)GetValue(referenceProperty); }
            set { SetValue(referenceProperty, value); }
        }
        public static readonly DependencyProperty referenceProperty =
            DependencyProperty.Register("Reference", typeof(TimerMode), typeof(SlideClockModeTrigger), new PropertyMetadata(TimerMode.Timer));

    }

    //可见性触发
    public class OverallStateBarStateTrigger : StateTriggerBase
    {
        public Visibility TargetStatus
        {
            get { return (Visibility)GetValue(TargetStatusProperty); }
            set { SetValue(TargetStatusProperty, value); }
        }
        public static readonly DependencyProperty TargetStatusProperty =
            DependencyProperty.Register("TargetStatus", typeof(Visibility), typeof(OverallStateBarStateTrigger), 
                new PropertyMetadata(Visibility.Visible));

        public Visibility BindPropVis
        {
            get { return (Visibility)GetValue(BindPropVisProperty); }
            set { SetValue(BindPropVisProperty, value); }
        }
        public static readonly DependencyProperty BindPropVisProperty =
            DependencyProperty.Register("BindPropVis", typeof(Visibility), typeof(OverallStateBarStateTrigger),
                new PropertyMetadata(Visibility.Collapsed,new PropertyChangedCallback(OnBindPropVisChanged)));
        private static void OnBindPropVisChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((OverallStateBarStateTrigger)d).SetActive(((Visibility)e.NewValue).Equals(((OverallStateBarStateTrigger)d).TargetStatus));
        }
    }

    public class StopNodesTrigger : StateTriggerBase
    {
        public TimeSpan ReferenceValue
        {
            get { return (TimeSpan)GetValue(ReferenceValueProperty); }
            set { SetValue(ReferenceValueProperty, value); }
        }
        public static readonly DependencyProperty ReferenceValueProperty =
            DependencyProperty.Register("ReferenceValue", typeof(TimeSpan), typeof(StopNodesTrigger), new PropertyMetadata(TimeSpan.Zero));

        public TimeSpan AimValue
        {
            get { return (TimeSpan)GetValue(AimValueProperty); }
            set { SetValue(AimValueProperty, value); }
        }
        public static readonly DependencyProperty AimValueProperty =
            DependencyProperty.Register("AimValue", typeof(TimeSpan), typeof(StopNodesTrigger), 
                new PropertyMetadata(TimeSpan.Zero, new PropertyChangedCallback(OnAimValueChanged)));
        private static void OnAimValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StopNodesTrigger snt = (StopNodesTrigger)d;
            snt.SetActive( snt.ReferenceValue.Equals(TimeSpan.Zero) && !((TimeSpan)e.NewValue).Equals(TimeSpan.Zero));
        }
    }

}
