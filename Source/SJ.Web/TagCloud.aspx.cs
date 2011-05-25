using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SJ.Core;
using System.Web.UI.HtmlControls;

namespace SJ.Web
{
    public partial class TagCloud : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowTagCloud();
        }

        private void ShowTagCloud()
        {
            IDictionary<string, UInt16> tagCloud = JobService.ExtractTagCloud();

            foreach (var item in tagCloud)
            {

                Label label = new Label()
                {
                    Text = string.Format("{0} ({1})", item.Key, item.Value)
                };

                divMain.Controls.Add(label);
            }
        }
    }
}