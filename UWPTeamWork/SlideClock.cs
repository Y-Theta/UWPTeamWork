using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Timers;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Media3D;


namespace UWPTeamWork
{
    public sealed class SlideClock : Control
    {

        public Timer Sec_Timer;
        public Timer MainTimer;

        private double Pointliner;
        private Point lastpos;

        //属性
        #region


        public bool TimerRuning
        {
            get { return (bool)GetValue(TimerRuningProperty); }
            set { SetValue(TimerRuningProperty, value); }
        }
        public static readonly DependencyProperty TimerRuningProperty =
            DependencyProperty.Register("TimerRuning", typeof(bool), typeof(SlideClock), new PropertyMetadata(false));


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
            if((double)e.NewValue > 360)
            {
                SlideClock k = (SlideClock)d;
                k.SecondsAng = 0;
            }
        }
        #endregion

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        public void Resume()
        {
            TimerRuning = true;
            Sec_Timer.Start();
            MainTimer.Start();
            VisualStateManager.GoToState(this, "Handle_Show", false);
        }

        public void Pause()
        {
            TimerRuning = false;
            Sec_Timer.Stop();
            MainTimer.Stop();
            SecondsAng = 360 - MainSeconds % 60 * 6;
            VisualStateManager.GoToState(this, "Handle_Hide", false);
        }

        public void Start()
        {
            if (MainSeconds == 0)
                return;
            TimerRuning = true;
            MainTimer.Start();
            Sec_Timer.Start();
            VisualStateManager.GoToState(this, "Handle_Show", false);
        }

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
        //private bool firstRingF = true;
        private double LastAngle = 0;

        protected override void OnManipulationStarting(ManipulationStartingRoutedEventArgs e)
        {
            Debug.WriteLine(SecondsAng);
            if (SecondsAng != 0)
            {
                VisualStateManager.GoToState(this, "SecNormal", false);
                VisualStateManager.GoToState(this, "SecToO", false);
                SecondsAng = 0;
            }
            OriginAngle = MinutesAng;
            base.OnManipulationStarting(e);
        }

        protected override void OnManipulationCompleted(ManipulationCompletedRoutedEventArgs e)
        {
            if (MinutesAng.Equals(359.5))
                MinutesAng = 360;
            base.OnManipulationCompleted(e);
        }

        protected override void OnManipulationDelta(ManipulationDeltaRoutedEventArgs e)
        {
            if (!e.IsInertial)
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
                    MainSeconds = (int)(Math.Round((double)angleOfLine / 6)) * 60;
                    MinutesAng = LastAngle = angleOfLine;
                    SetIconPathByAngle(angleOfLine);
                }
            }
            base.OnManipulationDelta(e);
        }

        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            Pointliner = Math.Sqrt(Math.Abs(e.GetCurrentPoint(this).Position.X - ActualWidth / 2) * Math.Abs(e.GetCurrentPoint(this).Position.X - ActualWidth / 2)
                  + Math.Abs(e.GetCurrentPoint(this).Position.Y - ActualHeight / 2) * Math.Abs(e.GetCurrentPoint(this).Position.Y - ActualHeight / 2));
            base.OnPointerPressed(e);
        }

        public SlideClock()
        {
            this.DefaultStyleKey = typeof(SlideClock);
            this.ManipulationMode = ManipulationModes.All;
            this.Loaded += OnLoaded;
            lastpos = new Point(200, 10);

            Sec_Timer = new Timer { Interval = 40 };
            Sec_Timer.Elapsed += Sec_Timer_Tick;

            MainTimer = new Timer { Interval = 1000 };
            MainTimer.Elapsed += Sec_Timer_Tick1;
        }

        private async void Sec_Timer_Tick1(object sender, ElapsedEventArgs e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                MainSeconds--;
                MinutesAng -= 0.1;
                SetIconPathByAngle(MinutesAng);
                Debug.WriteLine(SecondsAng + "  " + MainSeconds);
            });
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            
        }

        private async void Sec_Timer_Tick(object sender, object e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                SecondsAng += 0.24;
            });
        }

        #region 属性变化通知
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
