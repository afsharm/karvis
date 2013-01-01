using System.Collections.Generic;
using System.Web.Mvc;

namespace Karvis.Domain.ViewModels
{
    public class ExtractJobViewModel
    {
        public AdSource AdSource { get; set; }

        public IList<SelectListItem> SearchSource { get; set; }

        public string SelectedAdSource { get; set; }        
    }
}