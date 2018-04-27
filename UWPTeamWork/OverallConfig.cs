using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.UI.Xaml;

namespace UWPTeamWork
{
    class OverallConfig : INotifyPropertyChanged
    {

        private ElementTheme overalltheme;
        public ElementTheme OverallTheme
        {
            get { return overalltheme; }
            set
            {
                overalltheme = value;
                OnPropertyChanged("OverallTheme");
            }
        }

        public OverallConfig()
        {

        }

        #region 属性变化通知
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion    
    }
}
