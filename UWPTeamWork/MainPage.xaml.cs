using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace UWPTeamWork
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer timer;
        private int secAngle = 0;
        public int SecAngle
        {
            get { return secAngle; }
            set { secAngle = value > 59 ? 0 : value; }
        }

        public MainPage()
        {
            this.InitializeComponent();
            timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += timer_Tick;
        }

        private void timer_Tick(object sender, object e)
        {
            secAngle++;
            text.Text = secAngle.ToString();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }
    }
}
