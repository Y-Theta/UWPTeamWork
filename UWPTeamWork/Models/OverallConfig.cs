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
    class OverallConfig 
    {
        public static OverallConfig Instence = new OverallConfig();

        #region 主题颜色
        public event EventHandler OverallThemeChanged;
        private ElementTheme overalltheme = ElementTheme.Light;
        public ElementTheme OverallTheme
        {
            get { return overalltheme; }
            set
            {
                overalltheme = value;
                OverallThemeChanged?.Invoke(this,EventArgs.Empty);
            }
        }
        #endregion

        public OverallConfig()
        {

        }

    }
}
