using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karvis.Core;
using System.Web.UI.HtmlControls;

namespace Karvis.Web
{
    public interface IStatsAdSourceView : IView
    {
        void ShowAdSourceStat(IList<AdSourceStatDto> dto, int totalJobCount);
    }
}
