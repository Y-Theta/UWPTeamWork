using System.Diagnostics;
using Windows.ApplicationModel.Core;
using Windows.Data.Xml.Dom;
using Windows.UI;
using Windows.UI.Notifications;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPTeamWork
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TimerPage : Page 
    {

        public TimerPage()
        {
            InitTitleBar();
            this.InitializeComponent();
        }

        private void InitTitleBar()
        {
            var TitleBar = ApplicationView.GetForCurrentView().TitleBar;
            TitleBar.ButtonBackgroundColor = Colors.Transparent;
            TitleBar.ButtonForegroundColor = Colors.Transparent;

            TitleBar.ButtonHoverBackgroundColor = Color.FromArgb(100, 0, 0, 0);
            TitleBar.ButtonHoverForegroundColor = Colors.Transparent;

            TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            TitleBar.ButtonInactiveForegroundColor = Colors.Transparent;

            TitleBar.ButtonPressedBackgroundColor = Color.FromArgb(160, 0, 0, 0);
            TitleBar.ButtonPressedForegroundColor = Colors.Transparent;

            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            //在模板添加xml要的标题  
            string xml = "<toast>" +
                   "<visual>" +
                               "<binding template=\"ToastGeneric\">" +
                                   "<text>Title</text>" +
                                   "<text>Content</text>" +
                                   "<image placement=\"appLogoOverride\" src=\"ms-appx:///Assets/Square150x150Logo.scale-200.png\" />" +
                               "</binding>" +
                           "</visual>" +
                    "</toast>";
            doc.LoadXml(xml);
            //需要using Windows.Data.Xml.Dom;  

            //需要using Windows.UI.Notifications;  
            ToastNotification toast = new ToastNotification(doc);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
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
