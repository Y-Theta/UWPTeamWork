using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPTeamWork.tile
{
    public class PrimaryTile
    {
        public string time
        {
            set;
            get;
        } = "8:15 AM, Saturday";
        public string message
        {
            set;
            get;
        } = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed  do eiusmod tempor incididunt ut labore.";
        public string message2
        {
            set;
            get;
        } = "At vero eos et accusamus et iusto odio dignissimos ducimus  qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias  excepturi sint occaecati cupiditate non provident.";
        public string branding
        {
            set;
            get;
        } = "name";
        public string appName
        {
            set;
            get;
        } = "UWP";

        public void setValue(String time, String message1, String message2)
        {
            this.time = time;
            this.message = message1;
            this.message2 = message2;
        }
    }
}
