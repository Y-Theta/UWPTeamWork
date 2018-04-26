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

    public class ShowAdditionsCommand : ICommand
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
