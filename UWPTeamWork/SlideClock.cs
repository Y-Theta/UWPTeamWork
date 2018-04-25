using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;


namespace UWPTeamWork
{
    public sealed class SlideClock : Control
    {
        public enum TimerMode
        {
            Timer,
            StopWatch
        }
        public Timer Sec_Timer;
        public Timer MainTimer;
        public Timer StopwatchTimer;

        private double Pointliner;

        //属性
        #region 计时模式
        public TimerMode Mode
        {
            get { return (TimerMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(TimerMode), typeof(SlideClock), new PropertyMetadata(TimerMode.Timer));
        #endregion

        #region 是否有焦点
        public bool Focused
        {
            get { return (bool)GetValue(FocusedProperty); }
            set { SetValue(FocusedProperty, value); }
        }
        public static readonly DependencyProperty FocusedProperty =
            DependencyProperty.Register("Focused", typeof(bool), typeof(SlideClock), new PropertyMetadata(false));
        #endregion
    
        #region 指针运动方向
        public bool MinHDieection
        {
            get { return (bool)GetValue(MinHDieectionProperty); }
            set { SetValue(MinHDieectionProperty, value); }
        }
        public static readonly DependencyProperty MinHDieectionProperty =
            DependencyProperty.Register("MinHDieection", typeof(bool), typeof(SlideClock), new PropertyMetadata(true));
        #endregion

        #region 计时器是否计时
        public bool IsTimerRuning
        {
            get { return (bool)GetValue(IsTimerRuningProperty); }
            set { SetValue(IsTimerRuningProperty, value); }
        }
        public static readonly DependencyProperty IsTimerRuningProperty =
            DependencyProperty.Register("IsTimerRuning", typeof(bool), typeof(SlideClock), new PropertyMetadata(false,new PropertyChangedCallback(OnIsTimerRuningCHanged)));

        private static void OnIsTimerRuningCHanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SlideClock)d).MinHDieection = (bool)e.NewValue ? false : ((SlideClock)d).MinHDieection;
        }
        #endregion

        #region 指针路径
        public String MinPath
        {
            get { return (String)GetValue(MinPathProperty); }
            set { SetValue(MinPathProperty, value); }
        }
        public static readonly DependencyProperty MinPathProperty =
             DependencyProperty.Register("MinPath", typeof(String), typeof(SlideClock), new PropertyMetadata(""));
        #endregion

        #region 主计时     
        public int MainSeconds
        {
            get { return (int)GetValue(MainSecondsProperty); }
            set { SetValue(MainSecondsProperty, value); }
        }
        public static readonly DependencyProperty MainSecondsProperty =
            DependencyProperty.Register("MainSeconds", typeof(int), typeof(SlideClock), new PropertyMetadata(0,
                new PropertyChangedCallback(OnMainSecondsCHanged)));
        private static void OnMainSecondsCHanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((int)e.NewValue == 0)
                ((SlideClock)d).Pause();
        }
        #endregion

        #region 时间/m
        public double MinutesAng
        {
            get { return (double)GetValue(MinutesProperty); }
            set { SetValue(MinutesProperty, value); }
        }
        public static readonly DependencyProperty MinutesProperty =
            DependencyProperty.Register("MinutesAng", typeof(double), typeof(SlideClock), new PropertyMetadata(0.0, new PropertyChangedCallback(OnMinutesChanged)));
        private static void OnMinutesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SlideClock s = (SlideClock)d;
        }
        #endregion

        #region 时间/s
        public double SecondsAng
        {
            get { return (double)GetValue(SecondsAngProperty); }
            set { SetValue(SecondsAngProperty, value); }
        }
        public static readonly DependencyProperty SecondsAngProperty =
            DependencyProperty.Register("SecondsAng", typeof(double), typeof(SlideClock), new PropertyMetadata(0.0, new PropertyChangedCallback(OnSecondsChanged)));
        private static void OnSecondsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SlideClock k = (SlideClock)d;
            if ((double)e.NewValue < -360 || (double)e.NewValue > 360)
            {
                k.SecondsAng = 0;
            }
        }
        #endregion

        #region 时间/ms
        public int MillionSec
        {
            get { return (int)GetValue(MillionSecProperty); }
            set { SetValue(MillionSecProperty, value); }
        }
        public static readonly DependencyProperty MillionSecProperty =
            DependencyProperty.Register("MillionSec", typeof(int), typeof(SlideClock), new PropertyMetadata(0));
        #endregion


        #region 公共方法
        public void SetMode(TimerMode mode)
        {
            Mode = mode;
            switch (mode)
            {
                case TimerMode.Timer:
                    SwitchToTimer(true);
                    break;
                case TimerMode.StopWatch:
                    VisualStateManager.GoToState(this, "SwitchtoStopwatch", false);
                    SwitchToTimer(false);
                    break;
            }
        }

        public void Reset()
        {
            
        }

        public void CatchStop()
        {

        }

        public void StopwatchStart()
        {
            IsTimerRuning = true;
            MainTimer.Start();
            StopwatchTimer.Start();
            VisualStateManager.GoToState(this, "Handle_Show", false);
        }

        public void StopWatchPause()
        {
            IsTimerRuning = false;
            MainTimer.Stop();
            StopwatchTimer.Stop();
            SecondsAng = MainSeconds % 60 * 6 - 360;
            VisualStateManager.GoToState(this, "Handle_Hide", false);
        }

        public void Pause()
        {
            IsTimerRuning = false;
            Sec_Timer.Stop();
            MainTimer.Stop();
            SecondsAng = MainSeconds % 60 * 6 - 360;
            VisualStateManager.GoToState(this, "Handle_Hide", false);
        }

        public void Start()
        {
            if (MainSeconds == 0)
                return;
            IsTimerRuning = true;
            MainTimer.Start();
            Sec_Timer.Start();
            VisualStateManager.GoToState(this, "Handle_Show", false);
        }
        #endregion

        private void SetIconPathByAngle(double a)/*角度计算*/
        {
            var A = a * 2 * Math.PI / 360;
            var x = 180 * Math.Sin(A);
            var y = 180 * Math.Cos(A);
            x = 200 + x;
            y = 200 - y;
            if (a <= 180)
                MinPath = "M 200,20 A 180,180,0,0,1," + x.ToString() + "," + y.ToString();
            else
                MinPath = "M 200,20 A 180,180,0,1,1," + x.ToString() + "," + y.ToString();
        }

        private double OriginAngle = 0.0;
        private double LastAngle = 0;

        protected override void OnDoubleTapped(DoubleTappedRoutedEventArgs e)
        {
            switch (Mode)
            {
                case TimerMode.Timer:
                    if (IsTimerRuning) Pause();
                    else Start();
                    break;
                case TimerMode.StopWatch:
                    if (IsTimerRuning) StopWatchPause();
                    else StopwatchStart();
                    break;
            }
            base.OnDoubleTapped(e);
        }

        //指针进入
        protected override void OnPointerEntered(PointerRoutedEventArgs e)
        {
            Focused = true;
            base.OnPointerEntered(e);
        }

        //指针移出
        protected override void OnPointerExited(PointerRoutedEventArgs e)
        {
            Focused = false;
            base.OnPointerExited(e);
        }

        //滑动开始
        protected override void OnManipulationStarting(ManipulationStartingRoutedEventArgs e)
        {
            if (!IsTimerRuning && (Pointliner < ActualHeight * 3 / 5 && Pointliner > ActualHeight / 4))
            {
                if (SecondsAng != 0 && SecondsAng != 360)
                {
                    VisualStateManager.GoToState(this, "SecNormal", false);
                    VisualStateManager.GoToState(this, "SecToO", false);
                    SecondsAng = 0;
                }
                OriginAngle = MinutesAng;
            }
            base.OnManipulationStarting(e);
        }

        //滑动完成
        protected override void OnManipulationCompleted(ManipulationCompletedRoutedEventArgs e)
        {
            if (MinutesAng.Equals(359.5))
                MinutesAng = 360;
            base.OnManipulationCompleted(e);
        }

        //滑动
        protected override void OnManipulationDelta(ManipulationDeltaRoutedEventArgs e)
        {
            if (!e.IsInertial && !IsTimerRuning)
            {
                double angleOfLine = Math.Atan2((e.Position.Y - ActualHeight / 2), (e.Position.X - ActualWidth / 2)) * 180 / Math.PI + 90;
                if (Pointliner < ActualHeight * 3 / 5 && Pointliner > ActualHeight / 4)
                {
                    if (angleOfLine < 0)
                        angleOfLine = 360 + angleOfLine;
                    // Debug.WriteLine(e.Velocities.Linear + "  " + e.Delta.Translation.X + "  " + angleOfLine);
                    if ((angleOfLine < LastAngle || angleOfLine - LastAngle > 40) && angleOfLine > 180 && LastAngle < 180)
                        angleOfLine = 0;
                    if ((angleOfLine > LastAngle || LastAngle - angleOfLine > 180) && angleOfLine < 270 && LastAngle > 270)
                        angleOfLine = 359.5;
                    MinHDieection = angleOfLine > LastAngle;
                    MainSeconds = (int)(Math.Round((double)angleOfLine / 6)) * 60;
                    MinutesAng = LastAngle = angleOfLine;
                    SetIconPathByAngle(angleOfLine);
                }
                base.OnManipulationDelta(e);
            }
        }

        //按下
        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            if (!IsTimerRuning)
                Pointliner = Math.Sqrt(Math.Abs(e.GetCurrentPoint(this).Position.X - ActualWidth / 2) * Math.Abs(e.GetCurrentPoint(this).Position.X - ActualWidth / 2)
                      + Math.Abs(e.GetCurrentPoint(this).Position.Y - ActualHeight / 2) * Math.Abs(e.GetCurrentPoint(this).Position.Y - ActualHeight / 2));
            base.OnPointerPressed(e);
        }

        #region 计时器事件

        private async void MainTimer_Tick(object sender, ElapsedEventArgs e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                switch (Mode)
                {
                    case TimerMode.Timer:
                        MainSeconds--;
                        MinutesAng -= 0.1;break;
                    case TimerMode.StopWatch:
                        MainSeconds++;
                        MinutesAng += 0.1;break;
                }
                SetIconPathByAngle(MinutesAng);
               // Debug.WriteLine(SecondsAng + "  " + MainSeconds);
            });
        }

        private async void Sec_Timer_Tick(object sender, object e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                SecondsAng -= 0.24;
            });
        }

        private async void StopwatchTimer_Tick(object sender, object e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                SecondsAng += 0.06;
                MillionSec += 10;
            });
        }
        #endregion

        private void SwitchToTimer(bool t)
        {
            if (t)
            {
                StopwatchTimer.Dispose();
                Sec_Timer = new Timer { Interval = 40 };
                Sec_Timer.Elapsed += Sec_Timer_Tick;
            }
            else
            {
                Sec_Timer.Dispose();
                StopwatchTimer = new Timer { Interval = 10 };
                StopwatchTimer.Elapsed += StopwatchTimer_Tick;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Sec_Timer = new Timer { Interval = 40 };
            Sec_Timer.Elapsed += Sec_Timer_Tick;

            MainTimer = new Timer { Interval = 1000 };
            MainTimer.Elapsed += MainTimer_Tick;
        }

        public SlideClock()
        {
            this.DefaultStyleKey = typeof(SlideClock);
            this.ManipulationMode = ManipulationModes.All;
            this.Loaded += OnLoaded;

        }
    }
}
