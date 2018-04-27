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
                new PropertyMetadata(Visibility.Collapsed,new PropertyChangedCallback(OnAdditionButtonVisiblityChanged)));
        private static void OnAdditionButtonVisiblityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (((Visibility)e.NewValue).Equals(Visibility.Collapsed))
                VisualStateManager.GoToState((OverallStateBar)d, "AdditionButtons_Hide", false);
            else
                VisualStateManager.GoToState((OverallStateBar)d, "AdditionButtons_Show", false);
        }
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



        public OverallStateBar()
        {
            this.DefaultStyleKey = typeof(OverallStateBar);
        }
    }
}
