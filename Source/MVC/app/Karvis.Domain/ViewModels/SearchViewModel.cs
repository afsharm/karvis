using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Karvis.Domain.Dto;

namespace Karvis.Domain.ViewModels
{
    [Bind(Exclude = "SearchSource")]
    public class SearchViewModel
    {
        [Required]
        [DisplayName("عنوان شغل")]
        public string SearchTerm { get; set; }
        [DisplayName("تگ")]

        public string SearchTag { get; set; }
        [DisplayName("منبع")]


        public IEnumerable<SelectListItem> SearchSource { get; set; }

        [DisplayName("منبع ارسال شده به سرور")]
        public string AdSource { get; set; }

    }
}
