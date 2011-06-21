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
            var tagCloud = JobService.ExtractTagCloud();

            Image feedImage = new Image();

            foreach (var item in tagCloud)
            {

                HyperLink link = new HyperLink();

                string tagName = item.Key;
                UInt16 tagFrequency = item.Value;
                link.Text = FormatLtr(tagName, tagFrequency);
                link.NavigateUrl = string.Format("~/Job/JobList.aspx?tag={0}", item.Key);

                HyperLink feedLink = new HyperLink();
                feedLink.ImageUrl = "~/images/feed-tiny.jpg";
                feedLink.NavigateUrl = string.Format("~/feed.svc/bytag?tag={0}&format=rss", item.Key);

                divMain.Controls.Add(link);
                divMain.Controls.Add(new Label() { Text = "      " });
                divMain.Controls.Add(feedLink);
                divMain.Controls.Add(new Label() { Text = "      " });
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