using System.Collections.Generic;
using System.Web.Mvc;

namespace Karvis.Domain.ViewModels
{
    public class ExtractJobViewModel
    {
        public AdSource AdSource { get; set; }

        public IEnumerable<SelectListItem> SearchSource { get; set; }
    }
}