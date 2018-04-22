using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
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

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //var t = ToastTemplateType.ToastText02;
            ////在模板添加xml要的标题  
            //var content = ToastNotificationManager.GetTemplateContent(t);
            ////需要using Windows.Data.Xml.Dom;  
            //XmlNodeList xml = content.GetElementsByTagName("text");
            //xml[0].AppendChild(content.CreateTextNode("时间结束"));
            //xml[1].AppendChild(content.CreateTextNode("小文本"));
            ////需要using Windows.UI.Notifications;  
            //ToastNotification toast = new ToastNotification(content);
            //ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
