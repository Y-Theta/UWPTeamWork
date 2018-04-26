using System;
using System.Diagnostics;
using Windows.ApplicationModel.Core;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
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
    public sealed partial class TimerPage : Page ,IDisposable
    {

        public TimerPage()
        {
            InitTitleBar();
            this.InitializeComponent();
        }

        private void InitTitleBar()
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //XmlDocument doc = new XmlDocument();
            ////在模板添加xml要的标题  
            //string xml = "<toast>" +
            //       "<visual>" +
            //                   "<binding template=\"ToastGeneric\">" +
            //                       "<text>Title</text>" +
            //                       "<text>Content</text>" +
            //                       "<image placement=\"appLogoOverride\" src=\"ms-appx:///Assets/Square150x150Logo.scale-200.png\" />" +
            //                   "</binding>" +
            //               "</visual>" +
            //        "</toast>";
            //doc.LoadXml(xml); 
            //ToastNotification toast = new ToastNotification(doc);
            //ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SlideClock.Mode.Equals(SlideClock.TimerMode.StopWatch))
                SlideClock.SetMode(SlideClock.TimerMode.Timer);
            else
                SlideClock.SetMode(SlideClock.TimerMode.StopWatch);

        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                SlideClock.Dispose();
                disposedValue = true;
            }
        }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
