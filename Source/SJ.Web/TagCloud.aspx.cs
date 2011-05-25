using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SJ.Core;
using System.Web.UI.HtmlControls;
using Fardis;

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
                Label label = new Label();

                string tagName = item.Key;
                UInt16 tagFrequency = item.Value;
                label.Text = FormatLtr(tagName, tagFrequency);

                divMain.Controls.Add(label);
                divMain.Controls.Add(new Label() { Text = "      " });
            }
        }

        private string FormatLtr(string tagName, ushort tagFrequency)
        {
            //todo
            return string.Format("{0} \u200e({1})", tagName,
                FConvert.ToPersianDigit(tagFrequency.ToString()));
        }

        private string FormatRtl(string tagName, ushort tagFrequency)
        {
            return string.Format("\u202b{0} ({1})", tagName,
                FConvert.ToPersianDigit(tagFrequency.ToString()));
        }
    }
}