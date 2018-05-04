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
        XmlDocument Xml;

        public TimerPage()
        {
            InitToast();
            this.InitializeComponent();
            SlideClock.TimeOutEvent += SlideClock_TimeOutEvent;
        }

        private void SlideClock_TimeOutEvent(object sender, EventArgs e)
        {
            ToastNotification Toast = new ToastNotification(Xml);
            ToastNotificationManager.CreateToastNotifier().Show(Toast);
        }

        private void InitToast()
        {
            Xml = new XmlDocument();
            string xml = "<toast>" +
                             "<visual>" +
                               "<binding template=\"ToastGeneric\">" +
                                   "<text>Timer</text>" +
                                   "<text>Time Out</text>" +
                                   "<image placement=\"appLogoOverride\" src=\"ms-appx:///Assets/Square150x150Logo.scale-200.png\" />" +
                               "</binding>" +
                           "</visual>" +
                    "</toast>";
            Xml.LoadXml(xml);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
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
