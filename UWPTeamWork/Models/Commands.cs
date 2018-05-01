using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPTeamWork
{
    public class NavigateToClockCommand /* 导航命令 */ : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            ((Frame)parameter).Navigate(typeof(TimerPage));
        }
    }

    public class ShowPageCommand /* 导航命令 */ : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            ((Frame)parameter).Navigate(typeof(NotePage));
        }
    }

    public class SettingPageCommand /* 导航命令 */ : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            ((Frame)parameter).Navigate(typeof(SettingPage));
        }
    }

    public class SwitchCommand /* 切换 */ : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            SlideClock s = (SlideClock)parameter;
            s.SetMode((s.Mode.Equals(SlideClock.TimerMode.Timer) || s.Mode.Equals(SlideClock.TimerMode.Unknown))
                ? SlideClock.TimerMode.StopWatch : SlideClock.TimerMode.Timer);
        }
    }

    public class ShowStopListCommand /* 切换 */ : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            SlideClock s = (SlideClock)parameter;
            s.StopListVisibility = s.StopListVisibility.Equals(Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
        }
    }

    public class ChangeResCommand/* 更改主题 */ : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            var dics = App.Current.Resources.ThemeDictionaries;
            var dic = (ResourceDictionary)dics["Light"];
            dic.MergedDictionaries.Clear();
            dic.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri((string)parameter) });
            (Window.Current.Content as Frame).RequestedTheme = ElementTheme.Dark;
            (Window.Current.Content as Frame).RequestedTheme = ElementTheme.Light;
        }
    }

    public class ShowAdditionsCommand/* 显示附加按钮 */ : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            ((OverallStateBar)parameter).AdditionButtonVisiblity =
                ((OverallStateBar)parameter).AdditionButtonVisiblity.Equals(Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
