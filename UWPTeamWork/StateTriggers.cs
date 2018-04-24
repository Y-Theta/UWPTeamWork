using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace UWPTeamWork
{
    //刻度文字控制触发器
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

    //
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
                Debug.WriteLine("b");
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
                Debug.WriteLine("a");
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
}
