using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;


namespace UWPTeamWork
{
    public sealed class OverallStateBar : Control
    {
        #region 附加按钮状态
        public Visibility AdditionButtonVisiblity
        {
            get { return (Visibility)GetValue(AdditionButtonVisiblityProperty); }
            set { SetValue(AdditionButtonVisiblityProperty, value); }
        }
        public static readonly DependencyProperty AdditionButtonVisiblityProperty =
            DependencyProperty.Register("AdditionButtonVisiblity", typeof(Visibility), typeof(OverallStateBar),
                new PropertyMetadata(Visibility.Collapsed));
        #endregion

        #region 自动折叠
        public bool AutoRow
        {
            get { return (bool)GetValue(AutoRowProperty); }
            set { SetValue(AutoRowProperty, value); }
        }
        public static readonly DependencyProperty AutoRowProperty =
            DependencyProperty.Register("AutoRow", typeof(bool), typeof(OverallStateBar), new PropertyMetadata(true));
        #endregion

        #region 命令参数1
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(OverallStateBar), new PropertyMetadata(null));
        #endregion

        protected override void OnApplyTemplate()
        {
            var Timer = GetTemplateChild("Timer") as Button;
            Timer.Click += AdditionButton_Click;
            var Note = GetTemplateChild("Note") as Button;
            Note.Click += AdditionButton_Click;
            var Settings = GetTemplateChild("Settings") as Button;
            Settings.Click += AdditionButton_Click;
        }

        private void AdditionButton_Click(object sender, RoutedEventArgs e)
        {
            if (AutoRow)
                AdditionButtonVisiblity = Visibility.Collapsed;
        }

        public OverallStateBar()
        {
            this.DefaultStyleKey = typeof(OverallStateBar);
        }
    }
}
