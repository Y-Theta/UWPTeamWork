using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

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

        private DoubleAnimation SecB_RotateTransform_Storyboard_DoubleAnimation;
        private Storyboard SecB_RotateTransform_Storyboard;

        //属性
        #region 时间
        public TimeSpan Time
        {
            get { return (TimeSpan)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(TimeSpan), typeof(SlideClock), new PropertyMetadata(0));
        #endregion

        protected override void OnApplyTemplate()
        {
            SecB_RotateTransform_Storyboard = GetTemplateChild("SecB_RotateTransform_Storyboard") as Storyboard;
            SecB_RotateTransform_Storyboard_DoubleAnimation = GetTemplateChild("SecB_RotateTransform_Storyboard_DoubleAnimation") as DoubleAnimation;
            SecB_RotateTransform_Storyboard = GetTemplateChild("SecB_RotateTransform_Storyboard") as Storyboard;
            SecB_RotateTransform_Storyboard_DoubleAnimation = GetTemplateChild("SecB_RotateTransform_Storyboard_DoubleAnimation") as DoubleAnimation;
            SecB_RotateTransform_Storyboard = GetTemplateChild("SecB_RotateTransform_Storyboard") as Storyboard;
            SecB_RotateTransform_Storyboard_DoubleAnimation = GetTemplateChild("SecB_RotateTransform_Storyboard_DoubleAnimation") as DoubleAnimation;

            base.OnApplyTemplate();
        }

        private void SecB_ManipulationStarted(object sender, RoutedEventArgs e)
        {
        }

        protected override void OnPointerMoved(PointerRoutedEventArgs e)
        {

            base.OnPointerMoved(e);
        }

        protected override void OnTapped(TappedRoutedEventArgs e)
        {

            base.OnTapped(e);
        }

        protected override void OnManipulationDelta(ManipulationDeltaRoutedEventArgs e)
        {
            if (!e.IsInertial)
            {
                double angleOfLine = Math.Atan2((e.Position.Y - ActualHeight / 2), (e.Position.X - ActualWidth / 2)) * 180 / Math.PI;
                Debug.WriteLine(angleOfLine);
                SecB_RotateTransform_Storyboard_DoubleAnimation.To = angleOfLine;
                SecB_RotateTransform_Storyboard.Begin();
            }
            base.OnManipulationDelta(e);
        }

        public SlideClock()
        {
            this.DefaultStyleKey = typeof(SlideClock);
            this.ManipulationMode = ManipulationModes.All;
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
