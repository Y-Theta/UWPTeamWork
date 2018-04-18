using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace UWPTeamWork
{
    public sealed class SlideClock : Control
    {
        public enum HandleType
        {
            Hour = 0,
            Min = 1,
            Sec = 2
        }//当前选定指针
        private DoubleAnimation ClockDesk_RotateTransform_Storyboard_DoubleAnimation;
        private Storyboard ClockDesk_RotateTransform_Storyboard;
        private DoubleAnimation SecB_RotateTransform_Storyboard_DoubleAnimation;
        private Storyboard SecB_RotateTransform_Storyboard;
        private DoubleAnimation MinB_RotateTransform_Storyboard_DoubleAnimation;
        private Storyboard MinB_RotateTransform_Storyboard;
        private DoubleAnimation HourB_RotateTransform_Storyboard_DoubleAnimation;
        private Storyboard HourB_RotateTransform_Storyboard;

        private DispatcherTimer Sec_Timer;

        private double Pointliner;

        private double secAngle = 0;
        public double SecAngle
        {
            get { return secAngle; }
            set { secAngle = value > 360 ? 0 : value; }
        }
        private double deskAngle = 0;
        public double DeskAngle
        {
            get { return deskAngle; }
            set
            {
                if (value > 360)
                {
                    value = 0;
                    Sec_Timer.Stop();
                    Sec_Timer.Start();
                }
                deskAngle = value;
            }
        }

        //属性
        #region 选中的指针
        public HandleType SelectedH
        {
            get { return (HandleType)GetValue(SelectedHProperty); }
            set { SetValue(SelectedHProperty, value); }
        }
        public static readonly DependencyProperty SelectedHProperty =
            DependencyProperty.Register("SelectedH", typeof(HandleType), typeof(SlideClock), new PropertyMetadata(false, new PropertyChangedCallback(OnSecBTappedChanged)));

        private static void OnSecBTappedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        #endregion

        #region 时间/s
        public int Seconds
        {
            get { return (int)GetValue(SecondsProperty); }
            set { SetValue(SecondsProperty, value); }
        }
        public static readonly DependencyProperty SecondsProperty =
            DependencyProperty.Register("Seconds", typeof(int), typeof(SlideClock), new PropertyMetadata(0, new PropertyChangedCallback(OnSecondsChanged)));
        private static void OnSecondsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SlideClock a = (SlideClock)d;
            //Debug.WriteLine(e.OldValue);
            if ((int)e.NewValue == 40)
                a.Seconds = 0;
        }
        #endregion

        protected override void OnApplyTemplate()
        {
            ClockDesk_RotateTransform_Storyboard = GetTemplateChild("ClockDesk_RotateTransform_Storyboard") as Storyboard;
            ClockDesk_RotateTransform_Storyboard_DoubleAnimation = GetTemplateChild("ClockDesk_RotateTransform_Storyboard_DoubleAnimation") as DoubleAnimation;
            SecB_RotateTransform_Storyboard = GetTemplateChild("SecB_RotateTransform_Storyboard") as Storyboard;
            SecB_RotateTransform_Storyboard_DoubleAnimation = GetTemplateChild("SecB_RotateTransform_Storyboard_DoubleAnimation") as DoubleAnimation;
            MinB_RotateTransform_Storyboard = GetTemplateChild("MinB_RotateTransform_Storyboard") as Storyboard;
            MinB_RotateTransform_Storyboard_DoubleAnimation = GetTemplateChild("MinB_RotateTransform_Storyboard_DoubleAnimation") as DoubleAnimation;
            HourB_RotateTransform_Storyboard = GetTemplateChild("HourB_RotateTransform_Storyboard") as Storyboard;
            HourB_RotateTransform_Storyboard_DoubleAnimation = GetTemplateChild("HourB_RotateTransform_Storyboard_DoubleAnimation") as DoubleAnimation;
            
            base.OnApplyTemplate();
        }

        protected override void OnManipulationDelta(ManipulationDeltaRoutedEventArgs e)
        {
            if (!e.IsInertial)
            {
                double angleOfLine = Math.Atan2((e.Position.Y - ActualHeight / 2), (e.Position.X - ActualWidth / 2)) * 180 / Math.PI;
                if (Pointliner > ActualHeight / 3)
                {
                    HourB_RotateTransform_Storyboard_DoubleAnimation.To = angleOfLine;
                    HourB_RotateTransform_Storyboard.Begin();
                }
                else if (Pointliner > ActualHeight / 7)
                {
                    MinB_RotateTransform_Storyboard_DoubleAnimation.To = angleOfLine;
                    MinB_RotateTransform_Storyboard.Begin();
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

            Sec_Timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(25) };
            Sec_Timer.Tick += Sec_Timer_Tick;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Sec_Timer.Start();
        }

        private void Sec_Timer_Tick(object sender, object e)
        {
            Seconds++;
            if (Seconds % 10 == 0)
            {
                DeskAngle += 3;
                ClockDesk_RotateTransform_Storyboard_DoubleAnimation.To = DeskAngle;
                ClockDesk_RotateTransform_Storyboard.Begin();
            }
            SecAngle += 0.3;
            SecB_RotateTransform_Storyboard_DoubleAnimation.To = SecAngle;
            SecB_RotateTransform_Storyboard.Begin();
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
