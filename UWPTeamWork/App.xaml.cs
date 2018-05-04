using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace UWPTeamWork
{
    /// <summary>
    /// 提供特定于应用程序的行为，以补充默认的应用程序类。
    /// </summary>
    sealed partial class App : Application
    {
        private bool FileSaveComplete = false;
        /// <summary>
        /// 初始化单一实例应用程序对象。这是执行的创作代码的第一行，
        /// 已执行，逻辑上等同于 main() 或 WinMain()。
        /// </summary>
        public App()
        {
            LoadLocalSettings();
            this.InitializeComponent();
            this.Suspending += OnSuspending;   
        }

        /// <summary>
        /// 在应用程序由最终用户正常启动时进行调用。
        /// 将在启动应用程序以打开特定文件等情况下使用。
        /// </summary>
        /// <param name="e">有关启动请求和过程的详细信息。</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            ApplySettings();
            ApplicationView.PreferredLaunchViewSize = new Size(520, 700);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootFrame == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 从之前挂起的应用程序加载状态
                }
                
                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // 当导航堆栈尚未还原时，导航到第一页，
                    // 并通过将所需信息作为导航参数传入来配置
                    // 参数
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // 确保当前窗口处于活动状态
                Window.Current.Activate();
            }
        }

        private async void SaveLocalSettings()
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile f = await folder.CreateFileAsync("Setting.dat", CreationCollisionOption.ReplaceExisting);
            using (FileStream stream = new FileStream(f.CreateSafeFileHandle(), FileAccess.Write))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                RemoveEvent(OverallConfigManger.Instence, "WindowModeChanged");
                binaryFormatter.Serialize(stream, OverallConfigManger.Instence);
            }
            FileSaveComplete = true;
        }

        static void RemoveEvent<T>(T c, string name)
        {
            Delegate[] invokeList = GetObjectEventList(c, name);
            if (invokeList.Length != 0)
                foreach (Delegate del in invokeList) {
                    typeof(T).GetEvent(name).RemoveEventHandler(c, del);
                }
        }

        public static Delegate[] GetObjectEventList(object p_Object, string p_EventName)
        {
            FieldInfo _Field = p_Object.GetType().GetField(p_EventName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
            if (_Field == null)
                return null;
            object _FieldValue = _Field.GetValue(p_Object);
            if (_FieldValue != null && _FieldValue is Delegate) {
                Delegate _ObjectDelegate = (Delegate)_FieldValue;
                return _ObjectDelegate.GetInvocationList();
            }
            return null;
        }

        private async void LoadLocalSettings()
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile f = await folder.TryGetItemAsync("Setting.dat") as StorageFile;
            if (f != null)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(f.CreateSafeFileHandle(), FileAccess.Read))
                {
                    if (stream.CanRead && stream.Length != 0)
                        OverallConfigManger.Instence = (OverallConfigManger)binaryFormatter.Deserialize(stream);
                }
            }
        }

        private void ApplySettings()
        {
            #region 应用主题
            var dics = App.Current.Resources.ThemeDictionaries;
            var dic = (ResourceDictionary)dics["Light"];
            dic.MergedDictionaries.Clear();
            dic.MergedDictionaries.Add(new ResourceDictionary()
            { Source = new Uri(@"ms-appx:///Themes/" + OverallConfigManger.Instence.OverallTheme + ".xaml") });
            #endregion
        }

        /// <summary>
        /// 导航到特定页失败时调用
        /// </summary>
        ///<param name="sender">导航失败的框架</param>
        ///<param name="e">有关导航失败的详细信息</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// 在将要挂起应用程序执行时调用。  在不知道应用程序
        /// 无需知道应用程序会被终止还是会恢复，
        /// 并让内存内容保持不变。
        /// </summary>
        /// <param name="sender">挂起的请求的源。</param>
        /// <param name="e">有关挂起请求的详细信息。</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            SaveLocalSettings();
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: 保存应用程序状态并停止任何后台活动
            deferral.Complete();
        }

        
    }
}
