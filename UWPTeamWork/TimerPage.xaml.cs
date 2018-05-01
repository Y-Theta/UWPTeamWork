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
            SlideClock.PopTipChanged += SlideClock_PopTipChanged;
        }

        private void SlideClock_PopTipChanged(object sender, EventArgs e)
        {
            VisualStateManager.GoToState(this, "PopTipHide", false);
            VisualStateManager.GoToState(this, "PopTipShow", false);
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

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
