using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Timers;
using Windows.ApplicationModel.Core;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Notifications;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using UWPTeamWork.tile;
using xBindDataExample.Models;

namespace UWPTeamWork
{
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            NoteManager.init();
            var TitleBar = ApplicationView.GetForCurrentView().TitleBar;
            TitleBar.ButtonBackgroundColor = Colors.Transparent;
            TitleBar.ButtonForegroundColor = Colors.Transparent;

            TitleBar.ButtonHoverBackgroundColor = Color.FromArgb(100, 0, 0, 0);
            TitleBar.ButtonHoverForegroundColor = Colors.Transparent;

            TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            TitleBar.ButtonInactiveForegroundColor = Colors.Transparent;

            TitleBar.ButtonPressedBackgroundColor = Color.FromArgb(160, 0, 0, 0);
            TitleBar.ButtonPressedForegroundColor = Colors.Transparent;

            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size { Width = 200, Height = 200 });
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;

            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SetTitleBar(TitleBarBack);
        }

        private void Instence_OverallThemeChanged(object sender, EventArgs e)
        {
            RequestedTheme = ElementTheme.Light;
        }

        //更新磁贴
        private void UpdatePrimaryTile(object sender, RoutedEventArgs e)
        {
            PrimaryTile t = new PrimaryTile();
            //t.setValue可以设置磁贴时间以及信息
            XmlDocument xmlDoc = TileService.CreateTiles(t);

            TileUpdater updater = TileUpdateManager.CreateTileUpdaterForApplication();
            TileNotification notification = new TileNotification(xmlDoc);
            updater.Update(notification);
        }

    }
}
