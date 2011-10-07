using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karvis.Core
{
    public class AdSourceStatDto
    {
        public AdSource SiteSource { get; set; }

        public int Count { get; set; }

        public int Percent { get; set; }

        public string SiteSourceDescription { get; set; }
    }
}
