using System.Diagnostics;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            InitTitleBar();
            this.InitializeComponent();
        }

        private void InitTitleBar()
        {
            var view = ApplicationView.GetForCurrentView();
            view.TitleBar.ButtonBackgroundColor = Colors.Transparent;
            view.TitleBar.ButtonForegroundColor = Colors.Transparent;

            view.TitleBar.ButtonHoverBackgroundColor = Color.FromArgb(100, 0, 0, 0);
            view.TitleBar.ButtonHoverForegroundColor = Colors.Transparent;

            view.TitleBar.ButtonPressedBackgroundColor = Color.FromArgb(160, 0, 0, 0);
            view.TitleBar.ButtonPressedForegroundColor = Colors.Transparent;

            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
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

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            SlideClock.Pause();
        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            SlideClock.Resume();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            SlideClock.Start();
        }
    }
}
