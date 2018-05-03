using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace UWPTeamWork
{
    public sealed class StopList : ListView
    {

        #region Listholder
        public UIElement Holder {
            get { return (UIElement)GetValue(HolderProperty); }
            set { SetValue(HolderProperty, value); }
        }
        public static readonly DependencyProperty HolderProperty =
            DependencyProperty.Register("Holder", typeof(UIElement), typeof(StopList), new PropertyMetadata(null));
        #endregion

        protected override void OnTapped(TappedRoutedEventArgs e)
        {
            e.Handled = true;
            base.OnTapped(e);
        }

        public StopList()
        {
            this.DefaultStyleKey = typeof(StopList);
        }
    }
}
