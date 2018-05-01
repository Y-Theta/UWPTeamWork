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
    class OverallConfigManger
    {
        public static OverallConfigManger Instence = new OverallConfigManger();

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

        private String generalIns;
        public String GeneralIns
        {
            get => generalIns;
            set
            {
                generalIns = value;
            }
        }

        public OverallConfigManger()
        {

        }

    }
}
